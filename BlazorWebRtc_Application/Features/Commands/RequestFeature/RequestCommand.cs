using BlazorWebRtc_Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebRtc_Application.Features.Commands.RequestFeature
{
    public class RequestCommand : IRequest<bool>
    {
        public Status Status { get; set; } = Status.pending;
        public Guid ReceiverUserId { get; set; }
    }
}
