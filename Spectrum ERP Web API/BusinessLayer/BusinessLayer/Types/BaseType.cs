using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Types
{
    interface IBaseType 
    {

        object Save(Dictionary<string, object> Dic);

        object Fillvalues(Dictionary<string, Object> Dic);

        object LoadData(Dictionary<string, Object> Dic);

        object SearchData(Dictionary<string, Object> Dic);


        object Delete(Dictionary<string, Object> Dic);



    }
}
