I have attempted to create a DSL to programmatically (and easily) query postgresql databases.
My thought was to build in injection checking and compile time validation. I have not completely succeded in this task, but I think it is a decent step.
Obviously, to actually be helpful to anyone a lot more work would have to be done, but I figured this could server as a first step.

I have taken strong inspiration from the Graph example from Martin Fowler, and implemented it in a similar chaining style to his - albeit in c# and in a different context of course.
I have also borrow snippets of code from several places, but nothing more than a couple of lines at most from any single place (example is the regular expressions).

To run the example:
Navigate to root folder of project.
Run: dotnet build .
Run: dotnet run
Should be all that is needed, as the dependencies are included in the .csproj file (obviously, dotnet 3.1 or higher must be installed).

Last note: Since this is a very rough first draft which is meant to be easily ran without setting up system variables, the connection string is included directly in the code: I know this is bad (and I have broken several other best practices as well), please don't share the string through version control or other indirect means :)