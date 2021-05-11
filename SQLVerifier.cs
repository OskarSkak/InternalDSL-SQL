using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SQL_DSL
{
    public class SQLVerifier
    {
        public static void VerifyVarchar255(string item)
        {
            /*
             * Allows only a-z and A-Z and 0-9 in expression AND
             * Disallows strings with inline or block comments
             * Size per varchar(255) (1-255)
             */
            bool containsOnlyLatinLettersAndNumbers = new Regex("^([a-z]|[A-Z]|[0-9]){1,255}$").IsMatch(item);
            bool containsSQLStatements = new Regex("\b(ALTER|CREATE|DELETE|DROP|EXEC(UTE){0,1}|INSERT( +INTO){0,1}|MERGE|SELECT|UPDATE|UNION( +ALL){0,1})\b").IsMatch(item);
            bool containsInlineOrBlockComments = new Regex("/[\t\r\n]|(--[^\r\n]*)|(\\/\\*[\\w\\W]*?(?=\\*)\\*\\/)/gi").IsMatch(item);
            if (containsSQLStatements && !containsOnlyLatinLettersAndNumbers && !containsInlineOrBlockComments)
            {
                int exitCode = 2;

                if (containsSQLStatements)
                    Console.WriteLine("item: " + item + "\nContains illegal SQL statements...");
                if (!containsOnlyLatinLettersAndNumbers)
                    Console.WriteLine("item: " + item + "\nContains other characters than the legal a-z/A-Z/0-9...");
                if (containsInlineOrBlockComments)
                    Console.WriteLine("item: " + item + "\nContains illegal inline or block comments...");

                Console.WriteLine("Program terminated with exit code " + exitCode + "...");

                System.Environment.Exit(exitCode);
            }

        }
    }
}
