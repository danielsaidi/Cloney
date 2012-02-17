Cloney 0.9.2.1		2012-02-17
==============================

In this version, the console application file name mistake (it
was named Cloney.Console.exe in the last version) is fixed. It
is now named Cloney.exe once again.

I have also fixed the resource namespace crash. Thanks for the
fast feedback!



Cloney 0.9.2.0		2012-02-15
==============================

In this version, I have renamed the projects, so that the Core
project is now called Cloney. The console application has been
renamed to Cloney.Console, but the resulting exe file is still
named Cloney.exe.

I have experimented with the NuGet package generation and hope
that adding an empty reference tag will stop Cloney from being
added to references if you add it.



Cloney 0.9.1.0		2012-02-08
==============================

In this version, the clone operation does not require a source
and a target to be entered. In earlier versions, the operation
would abort, but now it will simply ask the user to enter them.

If Cloney is started by calling Cloney without parameters, the
application will now output a start message when it starts the
GUI application. This message instructs the user how to launch
the help command.

I have also added a new "settings" command, which will display
application settings. For now, the settings are read only, but
it would be nice to be able to manage them from the console.

Finally, I have added an error message that is printed when no
routine that could handle the command was found.

This version supports the following commands:

	<none>: Calling Cloney with no args will start the wizard.
	--help: Displays general help regarding how to use Cloney.
	--clone --source=x --target=y: Clones solution x to dir y.
	--settings: Prints application settings (readonly for now).


Cloney 0.9.0.0		2012-02-03
==============================

In this version, Cloney has been rewritten from scratch. It is
now almost entirely SOLIDified, except for the solution cloner
itself. Since it worked perfect in the last release, I think I
will just let it be for now and replace parts of it in a later
release.

This version features some rudimentary routines, including the
following ones:

	<none>: Calling Cloney with no args will start the wizard.
	--help: Displays general help regarding how to use Cloney.
	--clone --source=x --target=y: Clones solution x to dir y.

The wizard has undergone no changes, compared to the previous
version, except that it now calls the natice application with
other parameters. If you want to redesign it, let me know.



Cloney 0.5.0.0		2011-10-09
------------------------------

In this version, focus has been on structural changes and some
minor UI changes. The two applications should work like before,
except some small improvements.

If you download the source code and compare it to any previous
version, you will notice that Cloney has less classes now than
before. The reason to this, is that Cloney now uses the NExtra
library instead of duplicating the classes that exists in it.

The console application has been improved, to be more tolerant
when handling input arguments. It's now possible to use either
/ or \\ when specifying folder paths. earlier, / did cause the
application to crash.

The wizard has some small UI improvements. It is also possible
to enter text by hand into the text boxes, which will probably
make handling the application easier.



Older release notes have been removed
-------------------------------------

