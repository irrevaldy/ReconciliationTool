using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconciliationTool
{
    public class ReconciliationTool
    {
        //Private variable
        private List<Profile> listProfile = new List<Profile>();
        private List<Terminal> listTerm = new List<Terminal>();
        private SqlClass sqlObj;

        //Public variable
        public int inRetval = 0;

        public ReconciliationTool()
        {

        }

        public List<Terminal> listGetTerm()
        {
            return listTerm;
        }

        ~ReconciliationTool()
        {

        }   
    }
}


