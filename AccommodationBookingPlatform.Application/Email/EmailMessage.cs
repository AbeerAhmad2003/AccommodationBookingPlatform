namespace AccommodationBookingPlatform.Application.Email
{
    public record EmailMessage(
      string From,
      string To,
      string Subject,
      string? HtmlBody,
      string? PlainTextBody,
      List<(string FileName, byte[] Content, string ContentType)> Attachments
  );

}
