using Detetive.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detetive.Data.Repository.Base
{
    public abstract class BaseRepository
    {
        protected readonly DetetiveContext Context;

        public BaseRepository()
        {
            this.Context = new DetetiveContext();
        }
    }
}