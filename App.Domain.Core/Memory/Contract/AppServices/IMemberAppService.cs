using App.Domain.Core.Memory.Entities;
using App.Domain.Core.Memory.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Memory.Contract.AppServices
{
    public interface IMemberAppService
    {
        List<Member> GetAll();
        Member GetById(int id);
        Member Add(Member member);
        Member Update(Member member);
        bool Delete(int id);
        List<Member> Search(string query, Gender? gender = null, MembershipType? membershipType = null);
        bool IsNationalCodeDuplicate(string nationalCode);
        MembershipType GetMembershipTypeBasedOnScore(int score);


    }
}
