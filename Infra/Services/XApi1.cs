using Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using TinCan.LRSResponses;
using TinCan;
using System.Linq;

namespace Infra.Services
{
    public class XApi1 : IXApi
    {
        
        public bool SendStatement()
        {
            //Here you need to setup your SCROM CLoud information endpoint,Key and Password
            var lrs = new RemoteLRS(
      "https://cloud.scorm.com/lrs/INDFKJWFDA/sandbox/",
      "flQunzOGP_vjn9TE4N4",
      "cLWmEhkIPZE_xXoKk8Q"
  );

            //Defining the Statment Structure Starting with Actor Object
            var actor = new Agent();
            actor.mbox = "mailto:info@tincanapi.com";
            actor.name = "xAPi Name ";



            //Defining Verb Object , you Can always click on the Verb() and peek defenition to know whats Probs are there

            var verb = new Verb();
            verb.id = new Uri("http://adlnet.gov/expapi/verbs/started");
            verb.display = new LanguageMap();
            verb.display.Add("en-US", "started");



            //Defining Activity Object  

            var activity = new Activity();
            activity.id = "http://rusticisoftware.github.io/TinCan.NET";


           // I still need to figure out how to setup the Acitivty Details
            var activityDetails = new ActivityDefinition();
            LanguageMap name = new LanguageMap();
            name.Add("en-US", "userStarted Doing Somthing");
            activityDetails.name = name;
            activity.definition = activityDetails;




            //Setting up the Statement , adding the Acotr,verb,target('Activity')
            var statement = new Statement();
            statement.actor = actor;
            statement.verb = verb;
            statement.target = activity;

            StatementLRSResponse lrsResponse = lrs.SaveStatement(statement);
            if (lrsResponse.success)
            {
                // Success , you can check whats inside of the lesRespone Object 

                Console.WriteLine("Save statement: " + lrsResponse.content.id);
                return true;
            }
            else

            { 
                // Failed to save Statement
                return false;
            }
        }

        public List<Statement> GetStatements()
        {

            var lrs = new RemoteLRS(
   "https://cloud.scorm.com/lrs/INDFKJWFDA/sandbox/",
   "flQunzOGP_vjn9TE4N4",
   "cLWmEhkIPZE_xXoKk8Q"
);
            var query = new StatementsQuery();

            var agent = new Agent();
             agent.mbox = "mailto:info@tincanapi.com";
             query.agent=agent;
            //query.since = DateTime.ParseExact("2013-08-29 07:42:10Z", "u", System.Globalization.CultureInfo.InvariantCulture);
            //query.limit = 10;


            StatementsResultLRSResponse lrsResponse = lrs.QueryStatements(query);
            if (lrsResponse.success)
            {
                // List of statements available
                var result = lrsResponse.content.statements;
                return result.ToList();
            }
            else
            {
                return null;
            }
        }

    }
}
