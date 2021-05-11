using System;
using System.Collections.Generic;
using System.Text;

namespace SQL_DSL
{
    public class QueryBuilder
    {
        internal Query Query { get; set; }
        
        public QueryBuilder()
        {
            Query = new Query();
        }

        public static QueryBuilder query()
        {
            return new QueryBuilder();
        }

        public QueryTableBuilder table()
        {
            QueryTableBuilder builder = new QueryTableBuilder(this);
            Query.Table = builder.Table;
            return builder;
        }

        public void printQuery()
        {
            this.Query.PrintQuery();
        }
    }

    public class SelectionBuilder
    {
        private QueryBuilder qBuilder { get; set; }

        public SelectionBuilder(QueryBuilder _qBuilder)
        {
            qBuilder = _qBuilder;
        }

        public ConditionBuilder get(params string[] list)
        {
            if (list.Length == 0)
                throw new InvalidOperationException();

            for(int i = 0; i < list.Length; i++)
            {
                SQLVerifier.VerifyVarchar255(list[i]);
                this.qBuilder.Query.AddSelection(new Selection(list[i]));
            }

            return new ConditionBuilder(qBuilder);
        }
    }

    public class ConditionBuilder
    {
        private QueryBuilder qBuilder { get; set; }
        internal string condition { get; set; }
        public ConditionBuilder(QueryBuilder _queryBuilder)
        {
            this.qBuilder = _queryBuilder;
        }

        public ComparisonBuilder where(string param1)
        {
            SQLVerifier.VerifyVarchar255(param1);
            condition += param1;
            return new ComparisonBuilder(qBuilder, this);
        }

        public void PrintQuery()
        {
            this.qBuilder.printQuery();
        }

        public void sendQuery()
        {
            this.qBuilder.Query.QueryDB();
        }
    }

    public class ComparisonBuilder
    {
        private QueryBuilder queryBuilder { get; set; }
        private ConditionBuilder conditionBuilder { get; set; }

        private readonly string EQUALS = "=", LESS_THAN = "<", GREATER_THAN = ">", 
            GREATER_OR_EQUAL = ">=", LESSER_OR_EQUAL = "<=", NOT_EQUAL_TO = "<>";

        public ComparisonBuilder(QueryBuilder _queryBuilder, ConditionBuilder _condBuilder)
        {
            this.queryBuilder = _queryBuilder;
            this.conditionBuilder = _condBuilder;
        }

        public ConditionBuilder equals(string param)
        {
            SQLVerifier.VerifyVarchar255(param);

            this.conditionBuilder.condition += " " + this.EQUALS + " \'" + param + "\'";

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder lessThan(string param)
        {
            SQLVerifier.VerifyVarchar255(param);

            this.conditionBuilder.condition += " " + this.LESS_THAN + " \'" + param + "\'";

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder greaterThan(string param)
        {
            SQLVerifier.VerifyVarchar255(param);

            this.conditionBuilder.condition += " " + this.GREATER_THAN + " \'" + param + "\'";

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder greaterOrEqual(string param)
        {
            SQLVerifier.VerifyVarchar255(param);

            this.conditionBuilder.condition += " " + this.GREATER_OR_EQUAL + " \'" + param + "\'";

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder lesserOrEqual(string param)
        {
            SQLVerifier.VerifyVarchar255(param);

            this.conditionBuilder.condition += " " + this.LESSER_OR_EQUAL + " \'" + param + "\'";

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder notEqual(string param)
        {
            SQLVerifier.VerifyVarchar255(param);

            this.conditionBuilder.condition += " " + this.NOT_EQUAL_TO + " \'" + param + "\'";

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder equals(int param)
        {
            this.conditionBuilder.condition += " " + this.EQUALS + " " + param;

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder lessThan(int param)
        {
            this.conditionBuilder.condition += " " + this.LESS_THAN + " " + param;

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder greaterThan(int param)
        {
            this.conditionBuilder.condition += " " + this.GREATER_THAN + " " + param;

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder greaterOrEqual(int param)
        {
            this.conditionBuilder.condition += " " + this.GREATER_OR_EQUAL + " " + param;

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder lesserOrEqual(int param)
        {
            this.conditionBuilder.condition += " " + this.LESSER_OR_EQUAL + " " + param;

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

        public ConditionBuilder notEqual(int param)
        {
            this.conditionBuilder.condition += " " + this.NOT_EQUAL_TO + " " + param;

            this.queryBuilder.Query.AddCondition(new Condition(this.conditionBuilder.condition));

            return new ConditionBuilder(queryBuilder);
        }

    }

    public class QueryTableBuilder
    {
        internal Table Table { get; set; }
        private QueryBuilder QueryByuilder { get; set; }

        public QueryTableBuilder(QueryBuilder qBuilder)
        {
            this.Table = new Table();
            this.QueryByuilder = qBuilder;
        }

        public QueryTableBuilder name(String Name)
        {
            SQLVerifier.VerifyVarchar255(Name);
            this.Table.Name = Name;
            return this;
        }

        public SelectionBuilder connectionsString(string config)
        {
            this.Table.ConnectionString = config;
            return new SelectionBuilder(QueryByuilder);
        }
    }
}