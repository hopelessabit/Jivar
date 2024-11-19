using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Interfaces
{
    public interface IExcelExportSerivce
    {
        byte[] ExportProjectById(int id);
    }
}
