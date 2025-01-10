using App.Domain.Core.Memory.Contract.Repositories;
using App.Domain.Core.Memory.Contract.Services;
using App.Domain.Core.Memory.Entities;
using App.Domain.Core.Memory.Entities.Enum;

namespace App.Domain.Services.Memory.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public List<Member> GetAll()
        {
            return _memberRepository.GetAll();
        }

        public Member GetById(int id)
        {
            return _memberRepository.GetById(id);
        }

        public Member Add(Member member)
        {
            if (string.IsNullOrEmpty(member.NationalCode) || member.NationalCode.Length != 10)
            {
                throw new ArgumentException("National code must be exactly 10 digits.");
            }

            if (IsNationalCodeDuplicate(member.NationalCode))
            {
                throw new ArgumentException("The national code entered has already been registered.");
            }

            if (member.Gender == Gender.Male || member.Gender == Gender.Female)
            {
                member.MembershipType = GetMembershipTypeBasedOnScore(member.Score);
                return _memberRepository.Add(member);
            }
            else
            {
                throw new ArgumentException("Gender must be specified.");
            }
        }

        public Member Update(Member member)
        {
            if (member.Gender != Gender.Male && member.Gender != Gender.Female)
            {
                throw new ArgumentException("Gender must be specified.");
            }

            member.MembershipType = GetMembershipTypeBasedOnScore(member.Score);
            return _memberRepository.Update(member);
        }

        public bool Delete(int id)
        {
            return _memberRepository.Delete(id);
        }

        public List<Member> Search(string query, Gender? gender = null, MembershipType? membershipType = null)
        {
            var members = _memberRepository.GetAll()
                .Where(m => m.FirstName.StartsWith(query, StringComparison.OrdinalIgnoreCase) ||
                            m.LastName.StartsWith(query, StringComparison.OrdinalIgnoreCase));

            if (gender.HasValue)
            {
                members = members.Where(m => m.Gender == gender.Value);
            }

            if (membershipType.HasValue)
            {
                members = members.Where(m => m.MembershipType == membershipType.Value);
            }

            return members.ToList();
        }

        public bool IsNationalCodeDuplicate(string nationalCode)
        {
            return _memberRepository.GetAll().Any(m => m.NationalCode == nationalCode);
        }

        public MembershipType GetMembershipTypeBasedOnScore(int score)
        {
            if (score >= 1 && score <= 30)
            {
                return MembershipType.Bronze;
            }
            else if (score >= 31 && score <= 60)
            {
                return MembershipType.Silver;
            }
            else if (score >= 61 && score <= 100)
            {
                return MembershipType.Gold;
            }
            return MembershipType.Bronze;
        }
    }
}
