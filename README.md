Cloney
======

	Author:		Daniel Saidi [daniel.saidi@gmail.com]

Cloney is a Windows application that lets you clone .NET
solutions. It can be executed from the command prompt or
as a GUI application.

Cloney also features a Window Explorer plugin, that lets
you clone a solution by right-clicking the solution file
in the Windows Explorer.



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
grab the latest code from the GitHub repository.

Cloney can be executed as a console or a GUI application.
It also has a Window Explorer plugin that lets you clone
solutions by right-clicking an .sln file in the explorer.


### Using the Cloney GUI application

The easiest way to use Cloney is by starting the GUI app.
To start it, just double-click cloney.exe in the Windows
Explorer or launch cloney.exe without any arguments.

The GUI application has a text box for the source folder
and one for the target folder. Select the source and the
target folder then press "Clone" button, and you're done.

This is a simple, but nonflexible way to clone solutions.

You can also start a modal version of the GUI, using the
"cloney --modal [--source=]" command. When it uses modal,
Cloney will only display a source (if none provided) and
a target folder selector then start cloning the solution.
When the cloning operation is finished, Cloney exists.


### Using the Cloney console application

Using the Cloney console application provides you with a
lot more options than when using the GUI. 

The following command-line commands are supported by the
Cloney console app:

	*	cloney <no arguments>
		Launch cloney.exe without args to launch the GUI.
		
	*	cloney --clone --source=x --target=y OR
		Clone the solution in folder x to folder y.
		
	*	cloney --help
		Display a help message about how to use Cloney.
		
	*	cloney --install
		Install the Cloney Windows Explorer plugin.

	*	cloney --modal
		Start the Cloney GUI in modal (quick) mode.
		
	*	cloney --settings
		Display the current Cloney settings.
		
	*	cloney --uninstall
		Uninstall the Cloney Windows Explorer plugin.

Get in contact me if you have any ideas regarding how to
improve Cloney. A better GUI? More commands? Let me know!


### Using the Cloney Windows Explorer Plugin

Daniel Lee (@danlimerick) has created a nice, convenient
Windows Explorer plugin, that simplifies using Cloney in
the windows explorer.

To install the plugin, just type "cloney --install". The
Cloney plugin is then bound to the folder that installed
it. To uninstall the plugin, type "cloney --uninstall".

With the plugin properly installed, just right-click any
solution file in the Windows Explorer, then click "Clone
this solution using Cloney". This will open up a minimal
version of the GUI application. 



License
-------

Cloney is released under the MIT License. This basically
means you can do much anything you want with it.

Read  http://www.opensource.org/licenses/mit-license.php

If you use Cloney and like it, please spread the word. A
separate area at the web site is saved for user comments.

If you take a class or interface out of Cloney, keep the
author information in the source code.




