﻿
namespace Cf.Domain.Exceptions.Messages;

public static class DomainErrors
{
    public static class Advert
    {
        public static MessageInfo NotFound => new MessageInfo("Skelbimas nerastas");
        public static MessageInfo ActiveJobs => new MessageInfo("Skelbimo negalima istrinti, nes egzistuoja su juo susije darbai");
    }

    public static class Job
    {
        public static MessageInfo AdvertNotFound => new MessageInfo("Negalima sukurti darbo skelbimui, kuris neegzistuoja");
        public static MessageInfo NotFound => new MessageInfo("Darbas nerastas");
        public static MessageInfo IncorrectStatus => new MessageInfo("Negalimas statuso keitimas");
    }
}