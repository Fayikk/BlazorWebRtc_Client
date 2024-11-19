let currentCall = null;

export function initializePeer(peerId)
{
    return new Peer(peerId);
}


export function answerCall(peer, stream) {
    peer.on('call', (call) => {
        call.answer(stream);
        currentCall = call;
        call.on('stream', (remoteStream) => {
            document.getElementById('remoteVideo').srcObject = remoteStream;
        });
    });
}



export function callPeer(peer, peerId, stream) {
    console.log(peer)
    console.log(peerId)
    console.log(stream)

    const call = peer.call(peerId, stream);
    currentCall = call;
    call.on('stream', (remoteStream) => {
        document.getElementById('remoteVideo').srcObject = remoteStream;
    })
}


export function setLocalStream(elementId) {
    return navigator.mediaDevices.getUserMedia({ video: true, audio: true })
        .then((stream) => {
            document.getElementById(elementId).srcObject = stream;
            return stream;
        })
}


export function stopCall() {
    if (currentCall) {
        currentCall.close();
        currentCall = null;
    }

    const localVideo = document.getElementById("localVideo");
    const remoteVideo = document.getElementById("remoteVideo");


    if (localVideo && localVideo.srcObject) {
        localVideo.srcObject.getTracks().forEach(track => track.stop());
        localVideo.srcObject = null;
    }

    if (remoteVideo && remoteVideo.srcObject) {
        remoteVideo.srcObject.getTracks().forEach(track => track.stop());
        remoteVideo.srcObject = null;
    }
}