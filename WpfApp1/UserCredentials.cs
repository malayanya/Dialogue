using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletSimulator.Tools;

namespace WpfApp1
{
    [Serializable]
    public class UserCredentials
    {
        private const string PrivateKey = "<RSAKeyValue><Modulus>sr4l8EwMgPqPhRTK+dPTwGs9uYhDeUohSRL48ZDf85/5/Lo469WltKFaQvA2Msy92xq14YPPN6p6mQD7stVbinRd+hihnYoqfclYRMe+FG6jqu/QACl0N6JgwM7iKGiyzBjL1vkvcSoaqwbxD7QPHgrdNlkyP0z6Vz7j79PFEos=</Modulus><Exponent>AQAB</Exponent><P>6OkjEhjlvbDCuOl8e0Ep2zACTxkfSta8WFBmdvrinhQSowkT5xDXL0EFa/Z03XNUmjJ0xGe1aNCgG+6dDpTnSw==</P><Q>xHZTH4hXAv7uJsb/VHrcYOM5l4AyC+OxP7bhmAoGJGf4TpPxh+B0RhMxssrkc1d/72TIfRpuPbSLEqkqCSk5wQ==</Q><DP>SKFzK1CSTB4UCv/crr76Y3zMK4hlBryCDXQ9D7ta8frGeQr6puLMh9LZ8vnvJaOybUdwvFKu8pmkZDF7zrFGkw==</DP><DQ>J3ZNBAxyzds/IvLd3q4/DgcWTmQlqVW3CMFHVy7MRQvNSJtW7KAdOuYoGW2/rZtpy0BHNTnV4vcc6EaqduSdAQ==</DQ><InverseQ>4/jjapjJHdDqr5FG5a29ISgO6mRnjty6nrOisPNDi4336JdEKfAdtZvDUQoBAwKsV0oMvJ9RtPB2tS0hf5i8pA==</InverseQ><D>qcnyY/b5kbNxjasYvIQ5i3jTY2BLJ/YA9FcvXtiNw/DdGPMUiwGhrJnxEdD4yvyuBGm1CAmbV3d7icfjUBdYIe9VaZqPQ2FgYzI5DbB401+4z6Di7uKBVajLIOawlnufW4+K68T0EAFO2l9eo1RcU66W921G/pz6hObeUXt65QE=</D></RSAKeyValue>";

        private readonly string _encryptedPassword;

        public string Login { get; }
        public string Password => Encrypting.DecryptString(_encryptedPassword, PrivateKey);

        public UserCredentials(string login, string password)
        {
            Login = login;
            _encryptedPassword = Encrypting.EncryptString(password, PrivateKey);
        }
    }
}
