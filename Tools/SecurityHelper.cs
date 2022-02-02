using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;

namespace BurgerBuilderApi.Tools
{
	public class SecurityHelper
	{
		private IJwtAlgorithm algorithm;
		private IJsonSerializer serializer;
		private IBase64UrlEncoder urlEncoder;
		private IDateTimeProvider provider;
		private IJwtValidator validator;
		private IJwtDecoder decoder;
		private IJwtEncoder encoder;

		public SecurityHelper()
		{
			algorithm = new HMACSHA256Algorithm();
			serializer = new JsonNetSerializer();
			urlEncoder = new JwtBase64UrlEncoder();
			provider = new UtcDateTimeProvider();
			validator = new JwtValidator(serializer, provider);
			encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
			decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
		}
		const string secret = "GQDstcKsx0NHasdasdafeasd0uFiwDVvVBrk";
		public string GetToken(string username, string fullname, string refreshToken)
		{
			var payload = new Dictionary<string, object>
			{
					{ "username", username },
					{ "fullname", fullname },
					{ "refreshtoken", refreshToken }
			};

			return encoder.Encode(payload, secret);
		}

		public bool ValidateToken(string token)
		{
			try
			{
				var json = decoder.Decode(token, secret, verify: true);
				return true;
			}
			catch (TokenExpiredException)
			{
				return false;
			}
			catch (SignatureVerificationException)
			{
				return false;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public string GetUsername(string token)
		{
			var payload = decoder.DecodeToObject<IDictionary<string, object>>(token, secret, verify: true);
			return payload["username"].ToString();
		}
	}
}
