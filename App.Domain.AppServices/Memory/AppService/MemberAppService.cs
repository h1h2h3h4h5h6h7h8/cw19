using App.Domain.Core.Memory.Contract.AppServices;
using App.Domain.Core.Memory.Contract.Services;
using App.Domain.Core.Memory.Entities;
using App.Domain.Core.Memory.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Memory.AppService
{
    public class MemberAppService : IMemberAppService
    {
        private readonly IMemberService _memberService;

        public MemberAppService(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public List<Member> GetAll()
        {
            return _memberService.GetAll();
        }

        public Member GetById(int id)
        {
            return _memberService.GetById(id);
        }

        public Member Add(Member member)
        {
            return _memberService.Add(member);
        }

        public Member Update(Member member)
        {
            return _memberService.Update(member);
        }

        public bool Delete(int id)
        {
            return _memberService.Delete(id);
        }

        public bool IsNationalCodeDuplicate(string nationalCode)
        {
            return _memberService.IsNationalCodeDuplicate(nationalCode);
        }

        public List<Member> Search(string query, Gender? gender = null, MembershipType? membershipType = null)
        {
            return _memberService.Search(query, gender, membershipType);
        }

        public MembershipType GetMembershipTypeBasedOnScore(int score)
        {
            return _memberService.GetMembershipTypeBasedOnScore(score);
        }
    }
}
