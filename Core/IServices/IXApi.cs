using System;
using System.Collections.Generic;
using System.Text;
using TinCan;
using TinCan.LRSResponses;

namespace Core.IServices
{
    public interface IXApi
    {
        public bool SendStatement();
        public List<Statement> GetStatements();
    }
}
