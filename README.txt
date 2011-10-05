Cloney
======

Cloney is a Windows application that lets you clone .NET
solutions in no time, with just a few clicks. It is also
possible to use Cloney as a console application. 

Cloney copies all files and folders from a source folder
to a a target folder. When doing so, it will replace the
old namespace with the new one and ignore some files and
folders that should not be cloned (like git, tfs and svn
files and folders).

Cloney leaves you with a clones solution that is free of
any old, unwanted old stuff.


How to use Cloney
=================

When you execute Cloney.exe without any arguments, it is
started as a GUI application. If so, select a source and
a target folder, press "Clone" and you are done.

However, if you execute Cloney.exe from the console, and
with arguments, Cloney launches as a console application.
Launch it as such:

> Cloney.exe --source xxxxx --target yyyyy

For now, the console app is a nice-to-have feature, that
I have not put that much effort in. It could be extended
quite a lot.


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
