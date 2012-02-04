Cloney
======

	Author:		Daniel Saidi [daniel.saidi@gmail.com]

Cloney is a Windows application that lets you clone .NET
solutions in no time. It can be executed as a console as
well as a GUI application.

When Cloney clones a solution, it copies all folders and
files from a source folder (must contain a solution file)
to a target folder.

During a cloning operation, Cloney replaces the original
namespace with the name of the target folder, so that it
becomes the new namespace. Files and folders are renamed
if needed and file content is automatically adjusted.

Cloney will also ignore files and folders that shouldn't
be cloned (like git, tfs, svn files and folders). Cloney
only includes files and folders that are relevant to the
new solution.


Web resources
-------------

You can find more info about Cloney at the following web
resources:

	Web site:	http://danielsaidi.github.com/Cloney
	Project:	http://github.com/danielsaidi/Cloney
	Downloads:	http://github.com/danielsaidi/Cloney/downloads
	Issues:		http://github.com/danielsaidi/Cloney/issues
	Blog:		http://danielsaidi.wordpress.com
	
If you have any questions, do not hesitate to contact me.


Getting started
---------------

Cloney can be downloaded as a tag (source code), or as a
download bundle (pre-compiled) from GitHub. You can also
grab the latest source code by cloning the GitHub repo.

Cloney can be executed as a console or a GUI application.

If you start Cloney using the application icon, you will
start the GUI application. Use it to select a source and
a target folder, then press "Clone" to start cloning the
solution to the target folder.

For more options, use the console application. It has no
rich amount of features as of now, but has the following
commands:

	1.	cloney --help	
		Display a help message about how to use Cloney.
		
	2.	cloney <no arguments>
		Launch Cloney without args to start the GUI app.
		
	3.	cloney --clone --source=x --target=y
		Clone the solution in folder x to folder y.

Get in contact me if you have any ideas regarding how to
improve Cloney. A better GUI? More console commands? Let
me know!


License
-------

Cloney is released under the MIT License which basically
means you can do much anything you want with it. Read it
at http://www.opensource.org/licenses/mit-license.php

If you use Cloney and like it, please spread the word. A
separate area at the web site is saved for quotes, so if
you want to give me a quote, I'd love to publish it.

If you take a class or interface out of Cloney, keep the
author information in the source code.


