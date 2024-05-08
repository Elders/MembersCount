using Machine.Specifications;
using MembersCount;
using System;
using System.Collections.Generic;

namespace Tests
{
    [Subject(typeof(UserBinaryNumber))]
    public class When_the_munber_of_users_is_small
    {
        const int NUMBER_OF_USERS = 10;
        const int BITS_PER_USER = 3;

        static UserBinaryNumber user0;
        static UserBinaryNumber user1;
        static UserBinaryNumber user2;
        static UserBinaryNumber user3;

        static GlobalNumber systemCount;
        static int expectedResult;

        Establish context = () =>
        {
            //10 users, 3 bits per user => max userID = 9, max subscribers count = 8
            systemCount = new(NUMBER_OF_USERS, BITS_PER_USER);
        };

        It should_have_new_instance_of__BlackBox_ = () => systemCount.ShouldNotBeNull();

        class When_no_subscribers_are_added
        {
            Establish context = () =>
            {
                user0 = new UserBinaryNumber("urn:hawk:profile:0:rank:300");
            };

            It number_of_subscribers_should_be_zero = () => systemCount.CalculateSubscribers(user0.User).ShouldEqual(0);
        }

        class When_one_subscriber_is_added
        {
            Establish context = () =>
            {
                user1 = new UserBinaryNumber("urn:hawk:profile:1:rank:200");
            };

            Because of = () => systemCount.AddSubscriber(user1.User);

            It state_should_have_integer_value_same_as_expectedResult = () => systemCount.CalculateSubscribers(user1.User).ShouldEqual(1);
        }

        class When_many_subscribers_are_added
        {
            Establish context = () =>
            {
                user2 = new UserBinaryNumber("urn:hawk:profile:6:rank:00");
            };

            Because of = () =>
            {
                expectedResult = 6;
                for (int i = 0; i < expectedResult; i++)
                    systemCount.AddSubscriber(user2.User);
            };

            It number_of_subscribers_should_equal_expectedResult = () => systemCount.CalculateSubscribers(user2.User).ShouldEqual(expectedResult);
        }

        class When_one_subscriber_is_removed
        {
            static int addedUsers, removedUsers;

            Establish context = () =>
            {
                user0 = new UserBinaryNumber("urn:hawk:profile:3:rank:300");
            };

            Because of = () =>
            {
                addedUsers = 4;
                removedUsers = 1;
                for (int i = 0; i < addedUsers; i++)
                    systemCount.AddSubscriber(user0.User);

                for (int i = 0; i < removedUsers; i++)
                    systemCount.RemoveSubscriber(user0.User);
            };

            It number_of_subscribers_should_equal_expectedResult = () => systemCount.CalculateSubscribers(user0.User).ShouldEqual(addedUsers - removedUsers);
        }

        class When_many_subscribers_are_removed
        {
            static int addedUsers, removedUsers;

            Establish context = () =>
            {
                user0 = new UserBinaryNumber("urn:hawk:profile:3:rank:300");
            };

            Because of = () =>
            {
                addedUsers = 6;
                removedUsers = 4;
                for (int i = 0; i < addedUsers; i++)
                    systemCount.AddSubscriber(user0.User);

                for (int i = 0; i < removedUsers; i++)
                    systemCount.RemoveSubscriber(user0.User);
            };

            It number_of_subscribers_should_equal_expectedResult = () => systemCount.CalculateSubscribers(user0.User).ShouldEqual(addedUsers - removedUsers);
        }

        class When_userid_is_out_of_range
        {
            static Exception exception;

            Establish context = () =>
            {
                user1 = new UserBinaryNumber("urn:hawk:profile:21:rank:400");
            };

            Because of = () =>
            {
                exception = Catch.Exception(() =>
               {
                   systemCount.AddSubscriber(user1.User);
               });
            };

            It should_get_Index_out_of_range_exception = () => exception.ShouldBeOfExactType<IndexOutOfRangeException>();
        }

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
                user0 = new UserBinaryNumber("urn:hawk:profile:1:rank:000");
                user1 = new UserBinaryNumber("urn:hawk:profile:3:rank:100");
                user2 = new UserBinaryNumber("urn:hawk:profile:4:rank:200");
                user3 = new UserBinaryNumber("urn:hawk:profile:6:rank:300");

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
                user0 = new UserBinaryNumber("urn:hawk:profile:1:rank:000");
                user1 = new UserBinaryNumber("urn:hawk:profile:3:rank:100");
                user2 = new UserBinaryNumber("urn:hawk:profile:4:rank:200");
                user3 = new UserBinaryNumber("urn:hawk:profile:6:rank:300");

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
    }
}
