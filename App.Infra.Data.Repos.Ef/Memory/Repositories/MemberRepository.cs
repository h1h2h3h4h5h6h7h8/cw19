using App.Domain.Core.Memory.Contract.Repositories;
using App.Domain.Core.Memory.Entities;
using App.Infra.Data.Db.SqlServer.Ef.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.Memory.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContaxt _context;

        public MemberRepository(AppDbContaxt context)
        {
            _context = context;
        }

        public List<Member> GetAll()
        {
            return _context.Users.ToList();
        }

        public Member GetById(int id)
        {
            return _context.Users.FirstOrDefault(m => m.Id == id);
        }

        public Member Add(Member member)
        {
            _context.Users.Add(member);
            _context.SaveChanges();
            return member;
        }

        public Member Update(Member member)
        {
            var existingMember = _context.Users.FirstOrDefault(u => u.Id == member.Id);
            if (existingMember != null)
            {
                existingMember.FirstName = member.FirstName;
                existingMember.LastName = member.LastName;
                existingMember.Score = member.Score;
                existingMember.BirthDate = member.BirthDate;
                existingMember.NationalCode = member.NationalCode;
                existingMember.PhoneNumber = member.PhoneNumber;
                existingMember.Gender = member.Gender;
                existingMember.RegisterDate = member.RegisterDate;

                _context.Users.Update(existingMember);
                _context.SaveChanges();
                return existingMember;
            }
            return null;
        }


        public bool Delete(int id)
        {
            var member = GetById(id);
            if (member != null)
            {
                _context.Users.Remove(member);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
