namespace Cf.Domain.Exceptions.Messages;

public static class DomainErrors
{
    public static class Advert
    {
        public static MessageInfo NotFound => new MessageInfo("Skelbimas nerastas");
        public static MessageInfo ActiveJobs => new MessageInfo("Skelbimo negalima istrinti, nes egzistuoja su juo susije darbai");
        public static MessageInfo TooMuchImages => new MessageInfo("Skelbimas negali turėti daugiau nei 5 nuotraukas");
        public static MessageInfo ImageUploadFailed => new MessageInfo("Nepavyko įkelti nuotraukos į debesį");
        public static MessageInfo InvalidAdvertEndDate => new MessageInfo("Skelbimo pabaigos data turi būti ateities data");
    }

    public static class Job
    {
        public static MessageInfo AdvertNotFound => new MessageInfo("Negalima sukurti darbo skelbimui, kuris neegzistuoja");
        public static MessageInfo NotFound => new MessageInfo("Darbas nerastas");
        public static MessageInfo IncorrectStatus => new MessageInfo("Negalimas statuso keitimas");
    }

    public static class Service
    {
        public static MessageInfo NotFound => new MessageInfo("Could not find any content attached to a request body.");
        public static MessageInfo FailedUpload => new MessageInfo("Failed to process the PDF content. Try again.");
    }
}