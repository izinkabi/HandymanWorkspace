using HandymanDataLibrary.Internal.DataAccess;
using HandymanDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandymanDataLibrary.Internal
{
    class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var p = new { Id = Id };

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookUp", p, "DefaultConnection");

            return output;

        }
    }

    
}
