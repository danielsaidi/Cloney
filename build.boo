import System.IO

project_name = "Cloney"
solution_file = "Cloney.sln"
assembly_file = "Resources/SharedAssemblyInfo.cs"

build_folder  = "_tmpbuild_/"
build_version = ""
build_config  = env('config')

test_assemblies = (
   "Cloney.Tests/bin/${build_config}/Cloney.Tests.dll",
)


target default:
   pass

target zip, (compile, test, copy):
   zip("${build_folder}", "${project_name}.${build_version}.zip")
   rmdir(build_folder)
   
target deploy, (compile, test, copy):
   with FileList(build_folder):
    .Include("**/**")
    .ForEach def(file):
      file.CopyToDirectory("${project_name}.${build_version}")
   rmdir(build_folder)

target publish, (zip, publish_nuget, publish_github):
   pass


target compile:
   msbuild(file: solution_file, configuration: build_config, version: "4")
   
   //Probably a really crappy way to retrieve assembly
   //version, but I cannot use System.Reflection since
   //Phantom is old and if I recompile Phantom it does
   //not work. Also, since Phantom is old, it does not
   //find my plugin that can get new assembly versions.
   content = File.ReadAllText("${assembly_file}")
   start_index = content.IndexOf("AssemblyVersion(") + 17
   content = content.Substring(start_index)
   end_index = content.IndexOf("\"")
   build_version = content.Substring(0, end_index)

target test:
   nunit(assemblies: test_assemblies, enableTeamCity: true, toolPath: "resources/phantom/lib/nunit/nunit-console.exe", teamCityArgs: "v4.0 x86 NUnit-2.5.5")
   exec("del TestResult.xml")

target copy:
   rmdir(build_folder)
   mkdir(build_folder)
   
   File.Copy("README.md", "${build_folder}/README.txt", true)
   File.Copy("Release-notes.md", "${build_folder}/Release-notes.txt", true)
   
   with FileList(""):
    .Include("Cloney.Console/bin/${build_config}/*.*")
    .ForEach def(file):
      File.Copy(file.FullName, "${build_folder}/${file.Name}", true)


target publish_nuget:
   File.Copy("README.md", "Resources\\README.txt", true)
   File.Copy("Release-notes.md", "Resources\\Release-notes.txt", true)
   exec("nuget" , "pack Cloney\\Cloney.csproj -prop configuration=release")
   exec("nuget push Cloney.${build_version}.nupkg")
   exec("del *.nupkg")
   exec("del Resources\\README.txt")
   exec("del Resources\\Release-notes.txt")

target publish_github:
   exec("git add .")
   exec('git commit . -m "Publishing Cloney ' + "${build_version}" + '"')
   exec("git tag ${build_version}")
   exec("git push origin master")
   exec("git push origin ${build_version}")