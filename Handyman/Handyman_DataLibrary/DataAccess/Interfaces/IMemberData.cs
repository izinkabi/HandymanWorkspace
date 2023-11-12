using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces;

public interface IMemberData
{
    Member GetMember(string memberId);
    List<Member> GetMemberShips(int workId);
    void InsertMember(Member member);
}