using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper001.Data;
using Dapper001.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dapper001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDapperServices _dapper;
        public ValuesController(IDapperServices dapper)
        {
            _dapper = dapper;
        }


        [HttpPost]
        public async Task<int> Create(Detail data)
        {
            var dbparams = new DynamicParameters();
            
            dbparams.Add("Name", data.Name, DbType.String);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[Insert]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        [HttpGet]
        [Route ("{id}")]
        public async Task<Detail> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<Detail>($"Select * from [Details] where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<int> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute<int>($"Delete from [Details] Where Id = {Id}", null, commandType: CommandType.Text));
            return result;
        }
        [HttpGet]
        public Task<int> Count()
        {
            var totalcount = Task.FromResult(_dapper.Get<int>($"select COUNT(*) from [Details]", null,
                    commandType: CommandType.Text));
            return totalcount;
        }
        [HttpPatch(nameof(Update))]
        public Task<int> Update(Detail data)
        {
            var dbPara = new DynamicParameters();
            
            dbPara.Add("Name", data.Name, DbType.String);

            var updateArticle = Task.FromResult(_dapper.Update<int>("[dbo].[Update]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateArticle;
        }
    }
}
