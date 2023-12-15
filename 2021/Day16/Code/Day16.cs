using System.Text;

namespace Year2021
{
    public class Day16 : IDay
    {
        public object Sol1(string input)
        {
            string binary = string.Join(string.Empty,
                input.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                )
            );
            int versionTotal = 0;

            StringBuilder lastBits = new(16);
            int maxLastBitsLength = 3;

            Steps step = Steps.Ver;
            bool typeIsLiteral = false;
            bool lengthTypeIsNumberOfPackages = false;
            int numberOfPackages = 0;
            int lengthOfPackages = 0;
            //Boolean readNextPackage = true;

            // Int32 currentAmountOfPackagesRead;
            int currentNumberOfPackageCharsRead = 0;

            foreach (char bit in binary)
            {
                lastBits.Append(bit);
                if (lastBits.Length == maxLastBitsLength)
                {
                    switch (step)
                    {
                        case Steps.Ver:
                            versionTotal += Convert.ToInt32(lastBits.ToString(), 2);
                            step = Steps.Type;
                            maxLastBitsLength = 3; //maxLastBitsLength should already be 3 but lets be consistent here
                            break;
                        case Steps.Type:
                            typeIsLiteral = Convert.ToInt32(lastBits.ToString(), 2) == 4;
                            if (typeIsLiteral)
                            {
                                //readNextPackage = true;
                                step = Steps.SubPackets;
                                maxLastBitsLength = 5;
                            }
                            else
                            {
                                step = Steps.LengthTypeID;
                                maxLastBitsLength = 1;
                            }
                            break;
                        case Steps.LengthTypeID:
                            lengthTypeIsNumberOfPackages = lastBits.ToString() == "1";
                            step = lengthTypeIsNumberOfPackages ? Steps.SubPacketNumber : Steps.SubPacketLength;
                            maxLastBitsLength = lengthTypeIsNumberOfPackages ? 11 : 15;
                            break;
                        case Steps.SubPacketLength:
                            step = Steps.SubPackets;
                            maxLastBitsLength = 5;
                            lengthOfPackages = Convert.ToInt32(lastBits.ToString(), 2);
                            break;
                        case Steps.SubPacketNumber:
                            step = Steps.SubPackets;
                            maxLastBitsLength = 5;
                            numberOfPackages = Convert.ToInt32(lastBits.ToString(), 2);
                            // currentAmountOfPackagesRead = 0;
                            break;
                        case Steps.SubPackets:
                            if (typeIsLiteral)
                            {
                                int literalValue = Convert.ToInt32(lastBits.ToString(), 2);
                            }
                            else
                            {
                                int operatorValue = Convert.ToInt32(lastBits.ToString(), 2);
                            }
                            if (lastBits.ToString()[0] == '1')
                            {
                                step = Steps.SubPackets;
                            }
                            else
                            {
                                step = Steps.Ver;
                                currentNumberOfPackageCharsRead = 0;
                            }
                            break;
                    }
                    lastBits.Clear();
                }
                else if (lastBits.Length > maxLastBitsLength) throw new Exception("lastBits somehow exceeded the maxLastBitsLength");

                currentNumberOfPackageCharsRead++;
            }

            return versionTotal;
        }

        public object Sol2(string input)
        {


            return "";
        }

        enum Steps
        {
            Ver,
            Type,
            LengthTypeID,
            SubPacketLength,
            SubPacketNumber,
            SubPackets
        }
    }
}
