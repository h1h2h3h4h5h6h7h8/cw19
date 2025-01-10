using App.Domain.Core.Memory.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Memory.Contract.Repositories
{
    public interface IMemberRepository
    {
        List<Member> GetAll();
        Member GetById(int id);
        Member Add(Member member);
        Member Update(Member member);
        bool Delete(int id);
    }
}
