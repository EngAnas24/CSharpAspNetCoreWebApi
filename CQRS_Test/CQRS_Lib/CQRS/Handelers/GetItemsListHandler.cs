using CQRS_Lib.CQRS.Queries;
using CQRS_Lib.Data;
using CQRS_Lib.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Lib.CQRS.Handelers
{
    public class GetItemsListHandler : IRequestHandler<GetAllItemsQuery, List<Items>>
    {

        DBData dB;

        public GetItemsListHandler( DBData dB)
        {
            this.dB = dB;
        }

        public async Task<List<Items>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(dB.items.ToList());
        }
    }
}
