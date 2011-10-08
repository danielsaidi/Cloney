About Cloney
============

Author:		Daniel Saidi [daniel.saidi@gmail.com]
Web site:	http://danielsaidi.github.com/Cloney
Project:	http://github.com/danielsaidi/Cloney
Blog:		http://danielsaidi.wordpress.com


Cloney is a Windows application that lets you clone .NET
solutions in no time. Cloney can be run as both a wizard
as well as a GUI application.

Cloney copies all files and folders from a source folder
to a a target folder. When doing so, it will replace the
old namespace with the new one. It will also ignore some
files and folders that should not be cloned (such as git,
tfs, svn and ReSharper-related files and folders).

The result of a Cloney clone operation is a new solution
where the new namespace is applied everywhere, and where
stuff that strictly belong to the old one is ignored.


How to use Cloney
=================

If you execute Cloney.exe without arguments, it launches
a GUI wizard, that lets you clone solutions by selecting
a source and a target folder then press a "Clone" button.

If you execute Cloney.exe from the command line, you can
provid eit with arguments to make it launch as a console
application. For now, two arguments are required:

	> Cloney.exe --source:xxxxx --target:yyyyy

Both the console application and the GUI application can
be extended quite a lot.


Contact & downloads
===================

You will always find the latest source code and releases
at the GitHub page; http://github.com/danielsaidi/cloney

If you want to provide me with feedback or if you have a
lot of coool ideas, contact me at daniel.saidi@gmail.com
or visit the site - http://danielsaidi.github.com/Cloney


License
=======

Cloney is released under the MIT License. In short, feel
free to do whatever you want with it, but also feel free
to spread the word.

Check out the License text file for more information, or
read more here: http://en.wikipedia.org/wiki/MIT_License
