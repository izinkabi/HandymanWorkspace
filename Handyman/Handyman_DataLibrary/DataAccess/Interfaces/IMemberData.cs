using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces;

public interface IMemberData
{
    MemberModel GetMember(string memberId);
    List<MemberModel> GetMemberShips(int workId);
    void InsertMember(MemberModel member);
}