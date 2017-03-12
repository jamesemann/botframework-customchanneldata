using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;

namespace CustomChannelDataBot.Controllers
{
    [Serializable]
    public class ChannelDataDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedStart);
        }

        public async Task MessageReceivedStart(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            // Slack example
            // See https://api.slack.com/docs/messages
            //var reply = context.MakeMessage();

            //reply.Attachments = new List<Attachment>();
            //var attachments = new List<object>();
            //attachments.Add(new
            //{
            //    color = "#36a64f",
            //    pretext = "New bug raised",
            //    author_name= "James Mann",
            //    author_link = "https://github.com/jamesemann",
            //    author_icon = "https://avatars2.githubusercontent.com/u/6830648?v=3&s=400",
            //    title = "Server error on page load",
            //    title_link = "https://www.mywebsite.com/",
            //    text = "Optional text that appears within the attachment",
            //    fields = new[]
            //    {
            //        new
            //        {
            //            title = "Id",
            //            value = "12345"
            //        },
            //        new
            //        {
            //            title = "Severity",
            //            value = "High"
            //        },
            //        new
            //        {
            //            title = "Priority",
            //            value = "High"
            //        }
            //    }
            //});

            //reply.ChannelData = JObject.FromObject(new {attachments});

            //await context.PostAsync(reply);
            //context.Wait(MessageReceivedStart);

            // FB Messenger example
            // See https://developers.facebook.com/docs/messenger-platform/send-api-reference/airline-checkin-template
            var reply = context.MakeMessage();

            var attachment = new
            {
                type = "template",
                payload = new
                {
                    template_type = "airline_checkin",
                    intro_message = "Check-in is available now.",
                    locale = "en_US",
                    pnr_number = "ABCDEF",
                    checkin_url = "https://www.airline.com/check-in",
                    flight_info = new[]
                    {
                        new
                        {
                            flight_number = "f001",
                            departure_airport =
                            new
                            {
                                airport_code = "SFO",
                                city = "San Francisco",
                                terminal = "T4",
                                gate = "G8"
                            },
                            arrival_airport = new
                            {
                                airport_code = "SEA",
                                city = "Seattle",
                                terminal = "T4",
                                gate = "G8"
                            },
                            flight_schedule = new
                            {
                                boarding_time = "2016-01-05T15:05",
                                departure_time = "2016-01-05T15:45",
                                arrival_time = "2016-01-05T17:30"
                            }
                        }
                    }
                }
            };

            reply.ChannelData = JObject.FromObject(new { attachment });

            await context.PostAsync(reply);
            context.Wait(MessageReceivedStart);
        }
    }
}