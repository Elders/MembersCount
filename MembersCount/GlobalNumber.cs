namespace MembersCount
{
    public class GlobalNumber
    {
        int NumberOfUsers { get; set; }
        int BitsPerUser { get; set; }
        bool[] PceudoBigNumber { get; set; }

        public GlobalNumber(int numberOfUsers, int bitsPerUser)
        {
            NumberOfUsers = numberOfUsers;
            BitsPerUser = bitsPerUser;
            PceudoBigNumber = new bool[NumberOfUsers * BitsPerUser];
        }

        public void AddSubscriber(int userId)
        {
            try
            {
                // The last bit of the current position (least significant bit)
                var offset = userId * BitsPerUser; // offset

                for (int i = 0; i < BitsPerUser; i++)
                {
                    this.PceudoBigNumber[offset + i] = !this.PceudoBigNumber[offset + i];
                    if (this.PceudoBigNumber[offset + i])
                        break;
                }
            }

            catch (IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException($"Cannot add more subscribers: {ex.Message}");
            }
        }

        public void AddSubscriber(List<int> upLineUsers)
        {
            try
            {
                foreach (var user in upLineUsers)
                {
                    // The last bit of the current position (least significant bit)
                    var offset = user * BitsPerUser; // offset

                    for (int i = 0; i < BitsPerUser; i++)
                    {
                        this.PceudoBigNumber[offset + i] = !this.PceudoBigNumber[offset + i];
                        if (this.PceudoBigNumber[offset + i])
                            break;
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new IndexOutOfRangeException($"Cannot add more subscribers: {ex.Message}");
            }
        }

        public void RemoveSubscriber(int userId)
        {
            // The last bit of the current position (least significant bit)
            var offset = userId * BitsPerUser;

            for (int i = 0; i < BitsPerUser; i++)
            {
                this.PceudoBigNumber[offset + i] = !this.PceudoBigNumber[offset + i];
                if (!this.PceudoBigNumber[offset + i])
                    break;
            }
        }

        public void RemoveSubscriber(List<int> upLineUsers)
        {
            foreach (var user in upLineUsers)
            {
                // The last bit of the current position (least significant bit)
                var offset = user * BitsPerUser;

                for (int i = 0; i < BitsPerUser; i++)
                {
                    this.PceudoBigNumber[offset + i] = !this.PceudoBigNumber[offset + i];
                    if (!this.PceudoBigNumber[offset + i])
                        break;
                }
            }
        }

        public int CalculateSubscribers(int userId)
        {
            int count = 0;
            // The last bit of the current position (least significant bit)
            var currentLSB = userId * BitsPerUser;

            for (int i = 0; i < BitsPerUser; i++)
            {
                int temp = this.PceudoBigNumber[currentLSB + i] ? 1 : 0;
                count += (int)(temp * Math.Pow(2, i));
            }

            return count;
        }
    }
}
