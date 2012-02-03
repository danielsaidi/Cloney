Release notes
=============

Author:		Daniel Saidi [daniel.saidi@gmail.com]

Web site:	http://www.dotnextra.com
Project:	http://github.com/danielsaidi/NExtra
Downloads:	http://github.com/danielsaidi/NExtra/downloads
Issues:		http://github.com/danielsaidi/NExtra/issues
Blog:		http://danielsaidi.wordpress.com/category/nextra/



.NExtra 2.6.2.2		2012-02-03
------------------------------

I have moved build scripts and Phantom packages into the Resources
folder, to get them out of the way for you who are only interested
in the source code.

From now on, with more frequent releases, I will add release notes
only when I make functional changes to the library. Changes to any
files and folders out of the solution will not receive a node.



.NExtra 2.6.2.0		2012-02-02
------------------------------

I have added yet another class to the NExtra.IO namespace. The new
PathPatternMatcher class can be used to see if a file or directory
name matches a certain pattern, e.g. foo*.txt, *.txt, *foo*.



.NExtra 2.6.1.1		2012-01-31
------------------------------

This release features build scripts that makes building releases a
great deal simpler for me.

But why should you care? Well, I really do not know, except that I
have also optimized some classes according to the last NDepend run.



.NExtra 2.6.1.0		2012-01-31
------------------------------

I have added two new interface-based classes to Nextra.IO. One can
be used to work with directories and one with files. They are nice
to have if you need an abstract layer between your classes and the
file system.



.NExtra 2.6.0.1		2012-01-27
------------------------------

In this version, I have updated the readme file instructions. Also,
the return type of two assembly extension methods are changed from
IList to IEnumerable.



.NExtra 2.6.0.0		2012-01-10
------------------------------

This version is a real API breakier. If you are using any previous
versions of .NExtra, read this information carefully!


New stuff

I have added two new hash generators to NExtra.Security. They will
probably make the ones in NExtra.Web.Security useless, but the old
implementations are still there.

I have added two new SSN validators to the Validation namespace. A
new IValidator interface has been applied to all validators in the
namespace as well.

The new NExtra.Geo.IDistanceConverter interface has been extracted
from DistanceConverter. The same goes for IAngleConverter. So, now
you can do all kinds of crazy implementations. Go wild.


Breaking changes

Sooo, yeah. I have decided to add a lot of API breaking changes to
.NExtra. I will include all of them in this version so that future
releases will be stable...ish.

The most important API breaker to notice is that I have decided to
remove all "abstraction" namespaces. This makes it a lot easier to
use the various interfaces and implementations, since it no longer
requires using two (or more) namespaces.

The NExtra.ValidationAttributes namespace has been re-renamed, and
is now (once again) called Validation. SSN validators are moved to
a separate namespace, as has the Zip related validators.

The NExtra.Web.Abstractions.IVirtualPathProviderFacade was removed
as well, since it was not used anywhere in the library. If you can
not live without it, grab it from an earlier release.
	
The previously non-static NExtra.Testing.MetadataRegistrator class
has been made static. If you use it, just remove any instance.

Some unnecessary throws have been removed. The code will fail even
without them, so why have them?

And while I am already breaking the API everywhere, I have changes
several things in NExtra.Documentation. I am not even sure if this
namespace will live to see 2.6. Let me know if you use it.

And while...yeah, you probably get it by now. I have decided to do
a rewind on all the ICan interfaces. This will probably cause pain
to anyone using .NExtra (approx. 5 for the last version, so I hope
that this is a good time to perform all these changes).

NExtra.UI and all its classes (two useless ones) have been removed.
If you use them and need them still, you need serious help. 

Moving on to the NExtra.Web project, all HTML related classes have
been moved into a new Html namespace. Cookie handlers are moved to
NExtra.Web.Cookies. Html related http modules and response filters
are moved to NExtra.Html. Finally, SyndicationFeedLoader was moved
to the new NExtra.Syndication namespace, in the core project.

In NExtra.WinForms, I have moved the classes and interfaces around
into (I hope) a better structure.

Forgive me! Dear god, forgive me.



.NExtra 2.5.2.0		2011-12-06
------------------------------

I have added a new ValidationAttributes.PhoneNumberAttribute class.
It is really basic and allows an intial, optional +, followed by a
set of digits and spaces, followed by an optional -, then followed
by a last set of digits and spaces.

A brand new Cache section has been added to the NExtra project. It
has a basic ICache interface, a really simple dictionary cache and
a MemoryCache facade. NExtra.Web has a Cache section as well, with
an IHttpRuntimeCache interface and a HttpRuntime.Cache facade.

The NExtra.Extensions.ObjectExtensions class has a new non-generic
clone method that returns object instead of a certain type.

I have adjusted the Nextra.Web.Html5ElementConverter class so that
it adds no extra content to the end tag. Thanks, Petter :)

Finally, I have decided to let the demo section rot. The unit test
suite and the source code should be enough for developers who want
to learn .NExtra. Still, if anyone wants to keep the demos in good
shape, feel free to do so and push the changes to the public repo.



.NExtra 2.5.1.0		2011-11-27
------------------------------

I have fixed a bug in NExtra.Mvc.Testing.ControllerExtensions. The
CallWithModelValidation method used to assume that the MemberNames
property of each error did always contain an element. However, for
Compare validators, this is not the case.

The NExtra.Localization.HieararchicalResourceManagerFacade now has
two constructors instead of the previous one with one required and
one optional parameter. I will probably remove optional parameters
elsewhere in future versions.

In order to make client-side validation easier to use with all the
validation attributes in NExtra.ValidationAttributes, every regex-
based attribute now expose its internal expression, so that models
can use either the attribute directly or embed the expression in a
RegularExpressionAttribute (which works client-side).

The NExtra.Mvc.ActionFilters.OutputModelAttribute has been renamed
to JsonForQueryStringAttribute. It's quite more obvious.

The NExtra.Mvc.Extensions.WebViewPageExtensions has been rewritten
to use the HtmlHelper classes for local and global resources. Both
can be used, although only one should really be needed.

Finally and VERY IMPORTANT, is that no NExtra.ValidationAttributes
inherit from OptionalRegularExpressionAttribute from now on. These
classes now inherit directly from RegularExpressionAttribute. This
makes them OPTIONAL. If you need them to be required, combine them
with a Required attribute.

The affected validation attributes are:
 NExtra.ValidationAttributes.EmailAddressAtttribute
 NExtra.ValidationAttributes.GuidAtttribute
 NExtra.ValidationAttributes.IpAddressAttribute
 NExtra.ValidationAttributes.SwedishPostalCodeAttribute
 NExtra.ValidationAttributes.SwedishSsnAttribute

Since it is no longer used, the OptionalRegularExpressionAttribute
class has been removed. If anyone else uses it, well...let me know.
I have also removed some old, obsolete components as well.

A new documentation guideline I will try to stick to to reduce the
amount of comments is to have a comment per method for all methods
that are not part of a facade class like Console and ProcessFacade.
I have supressed 1591 to make related warnings disappear.



.NExtra 2.5.0.0		2011-10-19
------------------------------

Many new stuff and a new project name. I think this calls for some
minor version leaping.

I have changed the name from .NET Extensions to .NExtra, since the
old name was very general and said nothing about the library. This
new name will, I hope, work better.

I decided to switch from Google Code to GitHub since I host almost
all of my other personal projects there. Google Code has been (and
is still) great, but GitHub works better for me.

From now on, I will write less documentation for .NExtra since the
most of the code in this NExtra is self-explanatory. I have caught
myself writing absurd documentation only because all previous code
has been documented.

But what about the news?

This new version contains several new features, like an MVC helper,
logging utilities, a generic event arguments, a (borrowed) command
argument parser, several extension method additions etc. So, there
is a lot of new stuff to play around with.

The new generic NExtra.EventArgs class is a generic version of the
native EventArgs class. Use it to add a typed object with your arg.

The new NExtra.CommandLineArgumentParser class is a new, interface-
based version of a nice, clean implementation that can be found at:
http://www.codeproject.com/KB/recipes/command_line.aspx. Use it to
easily parse arguments that are sent to an application.
	
The new NExtra.Logging namespace contains a few new classes, enums
and interfaces that can be used when working with logging.

The new IConsole and ConsoleFacade are (so far) small and contains
two write methods for working towards the Console. I use them in a
private project, so they only has methods that I use, for now.

The same goes for Diagnostics.ProcessFacade and IProcess. They are
rather small, and can be used for working toward the Process class.
They should probably be extended as well.

I have added a couple of extension classes to NExtra.Extensions. I
have also added methods to already existing classes.

The NExtra.Web.Security.MembershipFacade has been extended to be a
more complete facade for the Membership and MembershipUser classes.

The new NExtra.Mvc.HtmlHelpers.FormBlockForHelper class can wrap a
form element (EditorFor, TextAreaFor etc.) in a structure with one
div with a LabelFor and one with the provided editor as well as an
editor-bound ValidationMessageFor.

The new NExtra.Mvc.HtmlHelpers.Conditional can be used to output a
value out of two possible ones, depending on a bool expression. It
can be used to avoid having to write all those nasty if-statements
in your views.



.NET Extensions 2.3.0.1		2011-08-17
--------------------------------------

I quickly applied a hot fix for the metadata provider and replaced
the 2.3.0 release with this one.



.NET Extensions 2.3.0.0		2011-08-17
--------------------------------------

In this version of .NET Extensions, I have made added some classes
that can be used to simplify working with metadata, unit tests and
localization...especially localization!


Braking changes:

The NExtra.Localization.Abstractions.ICanTranslate intrface
has been renamed to ITranslator and has changed its contract. This
new version will thus NOT WORK with any systems that use the older
version of the framework.

Another change that makes this version incompatible with any older
versions is that NExtra.Testing.MetadataTypeRegistrator has
been renamed to MetadataRegistrator.

I will be better at marking obsolete stuff as obsolete from now on.


Metadata:

I have added a brand new MetadataValidator class, that can be used
to validate metadata conditions, e.g. in unit tests.

And speaking of unit tests, I have added two fake classes that can
be used to setup fake HTTP contexts and requests. You can find the
classes in NExtra.Web.Testing.


Localization:

The next big thing about this release is localization and how .NET
Extensions can be used to translate stuff for various systems. The
classes and interfaces that handle localization are found within a
Localization root namespace in each project.

The new NExtra.Localization.ResourceManagers namespace will
contain resource managers that can translate language keys. Inside
it, I have placed two brand new classes; ResourceManagerFacade and
HierarchicalResourceManagerFacade.

The ResourceManagerFacade class will require an exact language key
match while the HierarchicalResourceManagerFacade class will strip
the provided key part by part until a translation is found, IF one
is found, e.g. Domain_User_UserName => User_UserName => UserName.

Finally, I did add a LocalizedDataAnnotationsModelMetadataProvider
metadata provider to NExtra.Mvc.Localizaion. Use it to make
ASP.NET MVC automatically translate any DisplayName attributes and
the ErrorMessage for any validation attributes. It can be set to a
friendly "preserve" mode, which keeps any already defined messages,
or be set to override everything that is already defined.

The provider will create a <Type.FullName>_<PropertyName> key when
translating display names. When translating error messages, it use
<Type.FullName>_<PropertyName>_<ValidationAttributeName>Error. The
HierarchicalResourceManagerFacade translator can be used to easily
translate a lot of properties and error messages with few language
keys, which can save a lot of work.


Avatars:

I have added two small classes, that can be used to handle avatars.
Under NExtra.Web.Avatar, you find a Gravatar and a Facebook
implementation.



.NET Extensions 2.2.2	2011-08-14
-----------------------------------

This released features some new functionality and has been cleaned
of old, deprecated components. Undocumented interfaces and classes
are now fully documented as well.

The ControllerExtensions.ControllerName(...) method was renamed to
Name(...) a while ago. The deprecated backup method is now deleted.

NExtra.Web.Security.Abstractions.IAuthenticationFacade is a
contract and not an implementation. It should thus not be called a
facade, since any classes that implement it could behave different
and not be a facade at all. The interface has thus been renamed to
IAuthenticationService. The facade interface is marked as obsolete.

As with the IAuthenticationFacade interface, the IMembershipFacade
interface has been renamed to IMembershipService, and IRolesFacade
has been renamed to...IRolesService (suprise!)

The NExtra.IO.Abstractions.ICanResolveFileSize interface is
removed, since it was (probably) the most worthless interface ever.



.NET Extensions 2.2.1	2011-08-09
----------------------------------

This small release features some new functionality, without any of
the structural change-mess of the most recent releases.

I have added a "required" constructor parameter to some classes in
the NExtra.ValidationAttributes namespace, so that they can
be set to optional as well. To avoid changing the classes' default
behavior, the default value of the parameter is true.

The NExtra.Extensions.StringExtensions.IsNullOrEmpty method
does not have a trim parameter anymore, since it made things weird.
It is IMPORTANT that you change any code where the methods is used,
if trimming is of concern to you. 

The NExtra.WebForms.WebControls.IFrame class has been a bit
optimized to reduce the number of inline instructions. Nothing big,
but maybe worth mentioning.

New classes/interfaces:
NExtra.Globalization.Abstractions.ICanTranslate

New methods:
NExtra.Extensions.IEnumerableExtensions.Contains
NExtra.Extensions.StringCollectionExtensions.AsEnumerable	



.NET Extensions 2.2.0	2011-06-13
----------------------------------

This release features a new NExtra.Web project. It contains
all System.Web dependent functionality that was previously defined
in the core project. The entire NExtra.Encryption namespace
has been moved here as well, but I will try to rewrite the MD5 and
SHA1 classes as well as Json, so that they do not need System.Web.

I have adjusted the IsSameDate function in DateTimeExtensions, and 
have also removed the RemoveTime method, since it is equal to Date.

I have replaced the NExtra.Extensions.FlagsExtensions class
with the NExtra.Extensions.EnumExtensions class. It extends
enums instead of ints/uints, which is a big improvement.

The NExtra.Extensions.StringExtensions.CountSubstring class
had a bug, that caused invalid counts when the sub string appeared
in sequence. This bug has been fixed.

NExtra.ValidationAttributes.SwedishSsnAttribute is modified
to allow selecting whether to use a dash or not.

In NExtra.MVC.Extensions.ControllerExtensions, I decided to
rename the Controller method to Name. The old method is now marked 
as deprecated, and will be removed...well, sometime.

I have added two new HTML helpers to NExtra.MVC.HtmlHelpers,
which simplifies working with global and local resource files.

I decided to delete the Json class, since it was nothing more than
a static facade for JavaScriptSerializer. Use this native class if
you need to serialize objects with JSON, or use any of the various
MVC helpers that exist, such as Json.Encode.

The demo project has been rewritten from scratch, and now uses the
Razor view engine instead of Spark. It also features demos for all
classes in .NET Extensions as well as the Web and Mvc projects.


New interfaces:
NExtra.Secutiry.Abstractions.ICanGenerateHashValue
NExtra.Web.Security.Abstractions.IAuthenticationFacade
NExtra.Web.Security.Abstractions.IMembershipFacade
NExtra.Web.Security.Abstractions.IRolesFacade

New classes:
NExtra.MVC.HtmlHelpers.GlobalResourceHtmlHelper
NExtra.MVC.HtmlHelpers.LocalResourceHtmlHelper
NExtra.ValidationAttributes.MinLengthAttribute
NExtra.Web.Security.FormsAuthenticationFacade
NExtra.Web.Security.MembershipFacade
NExtra.Web.Security.RolesFacade
NExtra.Web.Testing.FakeMembershipUser

Renamed/moved classes:
NExtra.Encryption.Md5Generator		=> NExtra.Web.Security.Md5Generator
NExtra.Encryption.Sha1Generator		=> NExtra.Web.Security.Sha1Generator
NExtra.Extensions.FlagsExtensions	=> NExtra.Extensions.EnumExtensions
NExtra.Web.Json

Renamed/moved methods:
NExtra.Mvc.Extensions.ControllerExtensions.Controller	=>	NExtra.Mvc.Extensions.ControllerExtensions.Name

Removed methods:
NExtra.Extensions.DateTimeExtensions.RemoveTime



.NET Extensions 2.1.2	2011-06-07
----------------------------------

This release is a hotfix, where the demo project has no references
to NExtra.MVC. I will upgrade the demo project to use MVC 3,
although that will probably require a Spark update as well.

I have also added a new solution folder called _Resources in which
I have placed the README and Release-notes text files.



.NET Extensions 2.1.1	2011-06-03
----------------------------------

This release features a few new classes and functions as well as a
structural change that has to be handled when upgrading from 2.1.0.

The major change is that the NExtra.Attributes namespace is
renamed to NExtra.ValidationAttributes. I think it is a lot
better, but it may require you to make some changes if you upgrade.

A quite cool addition is NExtra.Extensions.ObjectExtensions,
which makes it possible to clone an object of any kind, as long as
it is marked as Serializable.

New classes:
NExtra.Attributes.IpAddressAttribute (+ demo & tests)
NExtra.Extensions.ObjectExtensions (+ demo & tests)
NExtra.Extensions.StructExtensions (+ demo & tests)
NExtra.Mvc.ActionFilters.OutputModelFilter (+ demo)
NExtra.Mvc.Testing.ControllerExtensions
NExtra.WPF.Extensions.WindowExtensions

New methods:
NExtra.Extensions.IEnumerableExtensions.IsNullOrEmpty()
NExtra.Extensions.IEnumerableExtensions.IsSingle()



.NET Extensions 2.1.0	2011-05-27
----------------------------------

General changes
---------------

I have removed TODO unit tests. If they are needed, they will done,
but to have non-functional unit tests in the test projects is only
annoying. I have also renamed all private variables that did begin
with an _. It is a minor detail (and thanks to R#, minor work) but
perhaps good to know.


NExtra
-------------

I have added a new Attributes.GuidAttribute class, and moved every
attribute that was previously defined in NExtra.MVC to this
namespace.

The Attributes.SwedishSsnAttribute has been adjusted so that it is
not throwing an exception when too few digits are provided.

The Documentation namespace is completely new. It contains classes
that lets you extract documentation for assemblies, types, members
and methods. Use the main XmlDocumentationHandler class to quickly
get started and check out the Extractors sub-namespace if you need
to modify the default behavior....or make something completely new.

The Extensions.IOrderedQueryableExtensions class is removed, since
its extension methods could be moved into IQueryableExtensions.

The Extensions.AssemblyExtensions class has a new extension method
that can be used to return all types that inherit an interface. It
is called GetTypesThatImplement.

The Facades.FacadeBase class has been made non-abstract, so now it
is possible to create as many instances of it as you want.

The Pagination.PaginationContext.GetResult method has been renamed
to GetPaginationResult.

The Testing.MetadataTypeRegistrator class has been really improved.
Earlier versions just registered the existing MetadataTypes in the
EXECUTING assembly. However, since MetadataTypes may be defined in
any assembly that is part of a solution, the class now has methods
for registering MetadataTypes in any assembly and for any type.

The Web.Abstractions.ICanConvertHtml5Elements interface is renamed
to ICanConvertHtml. Its ConvertHtml5Elements method was renamed to
ConvertHtml. This affects the Html5ElementConverter class.

Web.Abstractions.ICanDetermineHtml5Support (I knooow that there is
no thing as HTML5 support) and Web.Html5Support are now renamed to
ICanDetermineHtml5ElementSupport and Html5ElementSupport.

Web.Abstractions.ICookieHandler & Web.HttpContextCookieHandler are
waaaay rewritten with more intuitive and flexible methods. The old
versions are NOT compatible with these new ones.

Web.Abstractions.ICanGetVirtualFile and Web.VirtualFileHandler are
removed in this new version, since the implementation was not that
good. If I make them better, I will add them once again.

The Web.Authentication namespace has been renamed to Web.Security.

The entire Web.Facades namespace has been removed, since the class
inside it was quite small and of little use.

The Web.Security classes as well as their corresponding interfaces
have been renamed as well, to IMembershipFacade / MembershipFacade
& IRolesFacade / RolesFacade. IMembershipFacade / MembershipFacade
has some new methods, but both classes be extended to fully mirror
the corresponding static classes. Maybe in another release :)


NExtra.MVC
-----------------

I have updated the MVC project to ASP.NET MVC 3.0.

I have removed the entire Facades folder, since I do not know (at
all) when I added it or what the hell I was thinking. If I find a
good explanations, maybe I will re-add the classes. So, thanks to
the removal, this DLL is really small so far.

Since the TagBuilder class is not available in ASP.NET MVC 3.0, I
have removed the ImageHelper class. If it is missed, I may create
it once more. Just let me know.

I have added a new HtmlHelper called HtmlStringHtmlHelper. It can
be used to present non-HTML string with HTML syntax. For now, the
class only replaces new lines with br tags, but its functionality
may change in the future.


NExtra.WebForms
----------------------

I have moved the StateBagExtensions class to the main project. It
is available in System.Web and now WebForms specific.


NExtra.WinForms
----------------------

No changes.


NExtra.WPF
----------------------

I chose to remove the IsX extension methods for the CheckBox and
RadioButton classes. So, now, they only contain a State() method
to retrieve their ThreeState state, instead of having to use the
nullable boolean IsChecked property.


Renamed methods/properties (screw deprecated :)
-----------------------------------------------

NExtra.Extensions.UriExtensions.GetServerRoot() => Root()
NExtra.Mvc.Extensions.ControllerExtensions.CurrentAction() => Action()
NExtra.Mvc.Extensions.ControllerExtensions.CurrentController() => Controller()
NExtra.WebForms.Extensions.ControlExtensions.GetHtml() => Html()