using TP2SI2.model;
using System.Collections.Generic;
namespace TP2SI2.mapper.interfaces
{
    interface ITeamMemberMapper : IMapper<TeamMember, KeyValuePair<int, int>, List<TeamMember>> { }
}
