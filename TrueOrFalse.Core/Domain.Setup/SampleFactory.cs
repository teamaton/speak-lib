using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.TrueOrFalse
{
    public static class SampleFactory
    {
        public static QuestionList GetSampleQuestions()
        {

            var sourceService = new SourceService();

            return new QuestionList
                       {
                           new Question
                               {
                                   Text = "Welche Geschwindigkeit erreicht ein durchschnittlicher Regentropfen?",
                                   Answer = new Answer() {Value = 35},
                                   Source = sourceService.GetInteressanteFaktenDe()
                               },
                           new Question
                               {
                                   Text =
                                       "Wie groß ist die Wahrscheinlichkeit, dass man in seinem Leben von einem Flugzeug getroffen wird, das vom Himmel stürzt? 1 zu ...",
                                   Answer = new Answer() {Value = 25000000},
                                   Source = sourceService.GetInteressanteFaktenDe()
                               },
                           new Question
                               {
                                   Text = "Wie viele Menschen wurden schon mal von Meteoritenteilen getroffen?",
                                   Answer = new Answer() {Value = 7},
                                   Source = sourceService.GetInteressanteFaktenDe()
                               },
                           new Question
                               {
                                   Text =
                                       "Wie viel Prozent britischer Schulkinder glauben, dass Deutschland das langweiligste Land in Europa ist?",
                                   Answer = new Answer() {Value = 57},
                                   Source = sourceService.GetInteressanteFaktenDe()
                               }
                       };
        }
    }
}
