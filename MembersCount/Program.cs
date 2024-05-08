namespace MembersCount
{
    public class Program
    {
        const int BitsPerUser = 23;
        const int NumberOfUsers = 5_000_000;

        public static void Main(string[] args)
        {
            GlobalNumber systemCount = new(NumberOfUsers, BitsPerUser);

            var init0 = "urn:hawk:profile:0:rank:300";
            var init1 = "urn:hawk:profile:1:rank:200";
            var init2 = "urn:hawk:profile:2:rank:400";
            var init3 = "urn:hawk:profile:3:rank:100";
            var init4 = "urn:hawk:profile:0:rank:200";
            var init5 = "urn:hawk:profile:23:rank:200";
            var init6 = "urn:hawk:profile:5:rank:00";
            var init7 = "urn:hawk:profile:1000000:rank:600";

            UserBinaryNumber user0 = new UserBinaryNumber(init0);
            UserBinaryNumber user1 = new UserBinaryNumber(init1);
            UserBinaryNumber user2 = new UserBinaryNumber(init2);
            UserBinaryNumber user3 = new UserBinaryNumber(init3);
            UserBinaryNumber user4 = new UserBinaryNumber(init4);
            UserBinaryNumber user5 = new UserBinaryNumber(init5);
            UserBinaryNumber user6 = new UserBinaryNumber(init6);
            UserBinaryNumber user7 = new UserBinaryNumber(init7);


            systemCount.AddSubscriber(user0.User);

            //systemCount.AddSubscriber(user2.User);
            //systemCount.AddSubscriber(user2.User);
            //systemCount.AddSubscriber(user2.User);
            //systemCount.AddSubscriber(user7.User);

            //systemCount.RemoveSubscriber(user3.User);


            Console.WriteLine(systemCount.CalculateSubscribers(user0.User));

            systemCount.AddSubscriber(user0.User);
            Console.WriteLine(systemCount.CalculateSubscribers(user0.User));
            systemCount.RemoveSubscriber(user0.User);
            Console.WriteLine(systemCount.CalculateSubscribers(user0.User));
            systemCount.AddSubscriber(user0.User);
            systemCount.AddSubscriber(user0.User);
            systemCount.AddSubscriber(user0.User);
            systemCount.AddSubscriber(user0.User);
            Console.WriteLine(systemCount.CalculateSubscribers(user0.User));
            systemCount.RemoveSubscriber(user0.User);
            systemCount.RemoveSubscriber(user0.User);
            systemCount.RemoveSubscriber(user0.User);
            systemCount.RemoveSubscriber(user0.User);
            systemCount.RemoveSubscriber(user0.User);

            Console.WriteLine(systemCount.CalculateSubscribers(user0.User));


            //Console.WriteLine(systemCount.CalculateSubscribers(user1.User));
            //Console.WriteLine(systemCount.C
            //alculateSubscribers(user2.User));
            //Console.WriteLine(systemCount.CalculateSubscribers(user3.User));
            //Console.WriteLine(systemCount.CalculateSubscribers(user4.User));
            //Console.WriteLine(systemCount.CalculateSubscribers(user5.User));
            //Console.WriteLine(systemCount.CalculateSubscribers(user6.User));
            //Console.WriteLine(systemCount.CalculateSubscribers(user7.User));



            //void AddSubscriber(int userId)
            //{
            //    // The last bit of the next position
            //    var nextLSB = (MinValue | 1) << ((userId + 1) * BitsPerUser);
            //    // The last bit of the current position (least significant bit)
            //    var currentLSB = (MinValue | 1) << (userId * BitsPerUser);
            //    // The section of bits for the current user.
            //    var groupOfBits = (MinValue | MaxValue) << (userId * BitsPerUser);

            //    // check for overflow
            //    if ((nextLSB & ((globalNumber & groupOfBits) + currentLSB)) == 0)
            //        globalNumber += currentLSB;
            //    else
            //        Console.WriteLine("overflow");
            //}

            //void RemoveSubscriber(int userId)
            //{
            //    // The last bit of the current position (least significant bit)
            //    var currentLSB = (MinValue | 1) << (userId * BitsPerUser);
            //    // The section of bits for the current user.
            //    var groupOfBits = (MinValue | MaxValue) << (userId * BitsPerUser);

            //    // check for underflow
            //    if ((((globalNumber & groupOfBits) - currentLSB) & ~groupOfBits) == 0)
            //        globalNumber -= currentLSB;
            //    else
            //        Console.WriteLine("underflow");
            //}

            //int CalculateSubscribers(int userId)
            //{
            //    UInt128 count = (globalNumber & ((MinValue | MaxValue) << (userId * BitsPerUser))) >> (userId * BitsPerUser);
            //    return (int)count;
            //}
        }
    }
}