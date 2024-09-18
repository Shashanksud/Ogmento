namespace OgmentoAPI.Domain.Authorization.Abstractions.DataContext
{
    public partial class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool? Used { get; set; }
        public int UserId { get; set; }

        public virtual UsersMaster User { get; set; }
    }
}
