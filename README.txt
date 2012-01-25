About Cloney
============

Author:		Daniel Saidi [daniel.saidi@gmail.com]
Web site:	http://danielsaidi.github.com/Cloney
Project:	http://github.com/danielsaidi/Cloney
Blog:		http://danielsaidi.wordpress.com


Cloney is a Windows application that lets you clone .NET
solutions in no time. Cloney can be run as a console app
as well as a GUI application.

Cloney copies all files and folders from a source folder
to a a target folder and replaces the old namespace with
the name of the target folder.

Cloney will also ignore files and folders that shouldn't
be cloned (like git, tfs, svn files/folders). This means
that a cloned solution does not contain parts of the old
solution that does not apply to the new one.


How to use Cloney
=================

If you execute Cloney.exe without arguments, it launches
a GUI application where you clone solutions by selecting
a source and target folder and then press a biiig button.

If you execute Cloney.exe from the command line, you can
provide it with arguments to make it launch as a console
application. It supports the following arguments:

	> Cloney.exe --help
	  Prints out a help text (much like this one)

	> Cloney.exe --clone xxxxx yyyyy
	  Clone a solution from the xxxxx folder to yyyyy

To modify the core behavior of Cloney, such as the files
and folders that it ignores, modify the config file.


Contact & downloads
===================

You will always find the latest source code and releases
at the GitHub page; http://github.com/danielsaidi/cloney

If you want to provide me with feedback or so, just send
me an e-mail to daniel.saidi@gmail.com or visit the site
http://danielsaidi.github.com/Cloney


License
=======

Cloney is released under the MIT License which basically
means that you can do much anything you want with it. It
also means that you are also free to spread the word, if
you happen to like it :)

Check out the License text file for more information, or
read more here: http://en.wikipedia.org/wiki/MIT_License
