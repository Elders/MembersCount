using Elders.Cronus;
using UniCom.Newton.Shared;

namespace MembersCount
{
    public sealed class UserBinaryNumber : Urn
    {
        private const string NSS_Profile = "profile";
        private const string NSS_Rank = "rank";
        private const byte BitsForUserId = 23;

        public int User { get; private set; }

        public UserBinaryNumber(string rawId) : base(rawId)
        {
            int userId = this.Extract(NSS_Profile, int.Parse);
            byte rank = RankToBinary(this.Extract(NSS_Rank, ushort.Parse));

            // using 4 MSB for Rank, remaining 23 bits for userId
            User = (ushort)((rank << BitsForUserId) | userId);
        }
        // 000_0000_0000_0000_0000_0000_0000

        private static byte RankToBinary(ushort rank)
        {
            // using switch, because we can add more ranks, in the middle, laterra
            return rank switch
            {
                100 => 0b0000_0001,
                200 => 0b0000_0010,
                300 => 0b0000_0011,
                400 => 0b0000_0100,
                500 => 0b0000_0101,
                600 => 0b0000_0110,
                _ => 0b0000,
            };
        }
    }
}
