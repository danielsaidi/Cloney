NExtra
======

	Author:		Daniel Saidi [daniel.saidi@gmail.com]

NExtra is an open-source library with extended functionality
for the .NET Framework. I add new things that I create in my
other projects if they are general and may help others.

To use NExtra, just download it from GitHub or NuGet and add
references to the parts of it that you want to use.

If I add functionality that I find at blogs, in forums etc.,
I will also add a link to the original implementation.



Web resources
-------------

You can find more information about .NExtra at the following
web resources:

	Web site:	http://www.dotnextra.com
	Project:	http://github.com/danielsaidi/NExtra
	Downloads:	http://github.com/danielsaidi/NExtra/downloads
	Issues:		http://github.com/danielsaidi/NExtra/issues
	Blog:		http://danielsaidi.wordpress.com/category/nextra/

If you have any questions, do not hesitate to contact me. To
report an issue, either use the issues page or e-mail me.

Contributions to the library are more than welcome. Just let
me know if you want to help out or if you need help.


Getting started
---------------

To get started with NExtra, either grab the source code from
GitHub or download a pre-compiled DLL from the download page
or simply get it from NuGet.

Once you have the compiled DLLs, just add a reference to the
projects you need. NExtra consists of the following projects:

	NExtra
	NExtra.Web
	NExtra.Mvc
	NExtra.Wpf
	NExtra.WebForms
	NExtra.WinForms
	
The NExtra project extends the System lib and can be used in
most projects. The other ones should only be added where the
target project depends on the corresponding System lib. Only
use NExtra.Web in projects that depend to System.Web. If not,
you may end up with unwanted dependencies.

The demo project is out of date. It may work for some of the
classes, but most will fail.


Documentation
-------------

The NExtra documentation can be found at the NExtra web site:

	http://www.dotnextra.com/documentation
	
The documentation is not auto-published for each new release.
It is a manual procedure, so if I haven't updated it, let me
know and I'll fix it.


License
-------

NExtra is released under the MIT License, which means that I
allow you to do much anything you want with it. Read more at
http://www.opensource.org/licenses/mit-license.php

If you use .NExtra and like it, please spread the word. Also,
do not take credit for other people's work. I add references
to all implementations that I use in .NExtra and assume that
you do the same.

If you take a class or interface out of .NExtra context, let
the author information remain in the source code.

