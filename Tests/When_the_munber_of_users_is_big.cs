using Machine.Specifications;
using MembersCount;
using System.Collections.Generic;

namespace Tests
{
    [Subject(typeof(UserBinaryNumber))]
    public class When_the_munber_of_users_is_big
    {
        const int NUMBER_OF_USERS = 1_000_000;
        const int BITS_PER_USER = 20;

        static UserBinaryNumber user0;
        static UserBinaryNumber user1;
        static UserBinaryNumber user2;
        static UserBinaryNumber user3;
        static UserBinaryNumber user4;
        static UserBinaryNumber user5;
        static UserBinaryNumber user6;
        static UserBinaryNumber user7;

        static GlobalNumber systemCount;
        static int expectedResult;

        Establish context = () =>
        {
            systemCount = new(NUMBER_OF_USERS, BITS_PER_USER);
        };

        class When_one_subscriber_is_added
        {
            Establish context = () =>
            {
                user1 = new UserBinaryNumber("urn:hawk:profile:99999:rank:200");
            };

            Because of = () => systemCount.AddSubscriber(user1.User);

            It state_should_have_integer_value_same_as_expectedResult = () => systemCount.CalculateSubscribers(user1.User).ShouldEqual(1);
        }

        class When_many_subscribers_are_added
        {
            Establish context = () =>
            {
                user6 = new UserBinaryNumber("urn:hawk:profile:6:rank:00");
            };

            Because of = () =>
            {
                expectedResult = 200;
                for (int i = 0; i < expectedResult; i++)
                    systemCount.AddSubscriber(user6.User);
            };

            It number_of_subscribers_should_equal_expectedResult = () => systemCount.CalculateSubscribers(user6.User).ShouldEqual(expectedResult);
        }

        class When_one_subscriber_is_removed
        {
            static int addedUsers, removedUsers;

            Establish context = () =>
            {
                user5 = new UserBinaryNumber("urn:hawk:profile:3453564:rank:300");
            };

            Because of = () =>
            {
                addedUsers = 400;
                removedUsers = 1;
                for (int i = 0; i < addedUsers; i++)
                    systemCount.AddSubscriber(user5.User);

                for (int i = 0; i < removedUsers; i++)
                    systemCount.RemoveSubscriber(user5.User);
            };

            It number_of_subscribers_should_equal_expectedResult = () => systemCount.CalculateSubscribers(user5.User).ShouldEqual(addedUsers - removedUsers);
        }

        class When_many_subscribers_are_removed
        {
            static int addedUsers, removedUsers;

            Establish context = () =>
            {
                user5 = new UserBinaryNumber("urn:hawk:profile:3:rank:300");
            };

            Because of = () =>
            {
                addedUsers = 6000;
                removedUsers = 4765;
                for (int i = 0; i < addedUsers; i++)
                    systemCount.AddSubscriber(user5.User);

                for (int i = 0; i < removedUsers; i++)
                    systemCount.RemoveSubscriber(user5.User);
            };

            It number_of_subscribers_should_equal_expectedResult = () => systemCount.CalculateSubscribers(user5.User).ShouldEqual(addedUsers - removedUsers);
        }

        //class When_userid_is_out_of_range
        //{
        //    static Exception exception;

        //    Establish context = () =>
        //    {
        //        user1 = new UserBinaryNumber("urn:hawk:profile:2324321000:rank:400");
        //    };

        //    Because of = () =>
        //    {
        //        exception = Catch.Exception(() =>
        //        {
        //            systemCount.AddSubscriber(user1.User);
        //        });
        //    };

        //    It should_get_Index_out_of_range_exception = () => exception.ShouldBeOfExactType<IndexOutOfRangeException>();
        //}

        //class When_subscribers_count_is_out_of_range
        //{
        //    static Exception exception;

        //    Establish context = () =>
        //    {
        //        user4 = new UserBinaryNumber("urn:hawk:profile:3:rank:400");
        //    };

        //    Because of = () =>
        //    {
        //        exception = Catch.Exception(() =>
        //        {
        //            for (int i = 0; i < BITS_PER_USER + 1; i++)
        //                systemCount.AddSubscriber(user4.User);
        //        });
        //    };

        //    It should_get_Index_out_of_range_exception = () => exception.ShouldBeOfExactType<IndexOutOfRangeException>();
        //}

        class When_one_subscriber_is_added_to_all_upline_users
        {
            static List<int> upLine = new List<int>();
            Establish context = () =>
            {
                user0 = new UserBinaryNumber("urn:hawk:profile:1576:rank:000");
                user1 = new UserBinaryNumber("urn:hawk:profile:3878:rank:100");
                user2 = new UserBinaryNumber("urn:hawk:profile:4:rank:200");
                user3 = new UserBinaryNumber("urn:hawk:profile:634546:rank:300");

                upLine = new List<int> { user0.User, user1.User, user2.User, user3.User };
            };

            Because of = () => systemCount.AddSubscriber(upLine);

            It state_should_have_integer_value_same_as_expectedResult = () =>
            {
                systemCount.CalculateSubscribers(user0.User).ShouldEqual(1);
                systemCount.CalculateSubscribers(user1.User).ShouldEqual(1);
                systemCount.CalculateSubscribers(user2.User).ShouldEqual(1);
                systemCount.CalculateSubscribers(user3.User).ShouldEqual(1);
            };
        }

        class When_one_subscriber_is_removed_from_all_upline_users
        {
            static List<int> upLine = new List<int>();
            Establish context = () =>
            {
                user0 = new UserBinaryNumber("urn:hawk:profile:178684:rank:000");
                user1 = new UserBinaryNumber("urn:hawk:profile:333:rank:100");
                user2 = new UserBinaryNumber("urn:hawk:profile:4576:rank:200");
                user3 = new UserBinaryNumber("urn:hawk:profile:666:rank:300");

                upLine = new List<int> { user0.User, user1.User, user2.User, user3.User };
            };

            Because of = () =>
            {
                systemCount.AddSubscriber(upLine);
                systemCount.RemoveSubscriber(upLine);
            };

            It state_should_have_integer_value_same_as_expectedResult = () =>
            {
                systemCount.CalculateSubscribers(user0.User).ShouldEqual(0);
                systemCount.CalculateSubscribers(user1.User).ShouldEqual(0);
                systemCount.CalculateSubscribers(user2.User).ShouldEqual(0);
                systemCount.CalculateSubscribers(user3.User).ShouldEqual(0);
            };
        }

        class When_adding_subscribers_to_multiple_uplines
        {
            static List<int> upLine0 = new List<int>();
            static List<int> upLine1 = new List<int>();
            static List<int> upLine2 = new List<int>();
            Establish context = () =>
            {
                user0 = new UserBinaryNumber("urn:hawk:profile:1576:rank:000");
                user1 = new UserBinaryNumber("urn:hawk:profile:3878:rank:100");
                user2 = new UserBinaryNumber("urn:hawk:profile:4:rank:200");
                user3 = new UserBinaryNumber("urn:hawk:profile:634546:rank:400");
                user4 = new UserBinaryNumber("urn:hawk:profile:3546:rank:300");
                user5 = new UserBinaryNumber("urn:hawk:profile:46:rank:500");
                user6 = new UserBinaryNumber("urn:hawk:profile:4546:rank:600");
                user7 = new UserBinaryNumber("urn:hawk:profile:23546:rank:00");

                upLine0 = new List<int> { user0.User, user1.User, user2.User, user3.User };
                upLine1 = new List<int> { user3.User, user6.User, user2.User, user6.User, user7.User };
                upLine2 = new List<int> { user0.User, user1.User, user2.User, user3.User, user4.User, user6.User, user6.User, user7.User };
            };

            Because of = () =>
            {
                systemCount.AddSubscriber(upLine0);
                systemCount.AddSubscriber(upLine1);
                systemCount.AddSubscriber(upLine2);
            };

            It state_should_have_integer_value_same_as_expectedResult = () =>
            {
                systemCount.CalculateSubscribers(user0.User).ShouldEqual(2);
                systemCount.CalculateSubscribers(user1.User).ShouldEqual(2);
                systemCount.CalculateSubscribers(user2.User).ShouldEqual(3);
                systemCount.CalculateSubscribers(user3.User).ShouldEqual(3);
                systemCount.CalculateSubscribers(user4.User).ShouldEqual(1);
                systemCount.CalculateSubscribers(user5.User).ShouldEqual(0);
                systemCount.CalculateSubscribers(user6.User).ShouldEqual(4);
                systemCount.CalculateSubscribers(user7.User).ShouldEqual(2);
            };
        }
    }
}
