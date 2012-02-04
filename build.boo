import System.IO
import System.Reflection

solution_file = "Cloney.sln"
build_folder  = "_tmpbuild_/"
build_version  = env('build.version')
build_config  = env('build.config')

test_assemblies = (
   "Cloney.Tests/bin/${build_config}/Cloney.Core.Tests.dll", , 
)


target default:
   pass
   
target zip, (compile, test, copy):
   zip("${build_folder}", "Cloney.${build_version}.zip")
   rmdir(build_folder)
   
target deploy, (compile, test, copy):
   with FileList(build_folder):
    .Include("**/**")
    .ForEach def(file):
      file.CopyToDirectory("Cloney.${build_version}")
   rmdir(build_folder)

target publish, (zip, publish_nuget, publish_github):
   pass


target compile:
   msbuild(file: solution_file, configuration: build_config, version: "4")

target test:
   nunit(assemblies: test_assemblies, enableTeamCity: true, toolPath: "resources/phantom/lib/nunit/nunit-console.exe", teamCityArgs: "v4.0 x86 NUnit-2.5.5")
   exec("del TestResult.xml")

target copy:
   rmdir(build_folder)
   mkdir(build_folder)
   
   File.Copy("README.md", "${build_folder}/README.txt", true)
   File.Copy("Release-notes.md", "${build_folder}/Release-notes.txt", true)
   
   with FileList(""):
    .Include("Cloney/bin/${build_config}/*.*")
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
   exec('git commit . -m "Publishing .Cloney ' + "${build_version}" + '"')
   exec("git tag ${build_version}")
   exec("git push origin master")
   exec("git push origin ${build_version}")