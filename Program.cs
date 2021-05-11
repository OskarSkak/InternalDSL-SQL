using System;

namespace SQL_DSL
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionsString = "postgres://uaqmxxsb:mw55O49wZtBE3WqdFeX2IfpvXcO0scLI@tai.db.elephantsql.com:5432/uaqmxxsb";

            Console.WriteLine("Example queries for the following table in postgreSQL:\n" +
                "name\taddress\tdean\tcountry\tcity\tranking\n*******************************************************\n");

            //Query for SDU in Odense
            QueryBuilder.query().
                table().
                    name("Universities").
                    connectionsString(connectionsString).
                    get("*").
                        where("name").
                            equals("SDU").
                        where("City").
                            equals("Odense").
                    sendQuery();

            //Query for universities with ranking better than 150
            QueryBuilder.query().
                table().
                    name("Universities").
                    connectionsString(connectionsString).
                    get("name, ranking").
                        where("ranking").
                            lessThan(150).
                    sendQuery();
                    
        }
    }
}
