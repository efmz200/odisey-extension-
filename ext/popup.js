
var spot_tok="BQBZwwgz9wTp1016sOVF9HuGdOtJK7LS-zLGChJuAAxwazHkG41wQMWsJi-YcQEpHwR3idtWChIAqXCvnVWWyYaIEgfm3L6vaR3sOJv8zq7T2l2DsHtwzQtvw-JI6F0AZU61vJR9hoMMx19zZPsxwz5NDEc-fxh_I9gyKHxYZdH3TnSy7VHI-lk"

chrome.runtime.onMessage.addListener((request,sender,sendResponse)=>{
    window.open("", "_self")
    document.getElementById('reproductor').src="https://open.spotify.com/embed/track/"+request.spotifyID

})
//window.onSpotifyWebPlaybackSDKReady = () => {
//    
//    const token = spot_tok;
//    const player = new Spotify.Player({
//      name: 'Odissey player',
//      getOAuthToken: cb => { cb(token); }
//    });
//    
//
//    // Error handling
//    player.addListener('initialization_error', ({ message }) => { console.error(message); });
//    player.addListener('authentication_error', ({ message }) => { console.error(message); });
//    player.addListener('account_error', ({ message }) => { console.error(message); });
//    player.addListener('playback_error', ({ message }) => { console.error(message); });
//
//    // Playback status updates
//    //player.addListener('player_state_changed', state => { console.log(state); });
//
//    // Ready
//    player.addListener('ready', ({ device_id }) => {
//      console.log('Ready with Device ID', device_id);
//      chrome.runtime.onMessage.addListener((request,sender,sendResponse)=>{
//        document.getElementById('nombre').innerHTML="Nombre: "+request.name
//        document.getElementById('artista').innerHTML="Artista: "+request.artist
//        document.getElementById('album').innerHTML="Album: "+request.album
//        document.getElementById('img_alb').src=request.imageURL
//        
//        fetch("https://api.spotify.com/v1/me/player/play", {
//        method: "PUT",
//        headers: {
//            "Accept": "application/json",
//            "Content-Type": "application/json",
//            Authorization: "Bearer " + spot_tok,
//        },
//        body: JSON.stringify({
//            uris: ["spotify:track:"+request.spotifyID],
//        }),
//    })
//    });
//    });
//
//    // Not Ready
//    player.addListener('not_ready', ({ device_id }) => {
//      console.log('Device ID has gone offline', device_id);
//    });
//
//    // Connect to the player!
//    player.connect();
//
//    document.getElementById('button_play').onclick=function(){
//        player.togglePlay().then(() => {
//            console.log('Toggled playback!');
//          });   
//    };
//    document.getElementById('button_vol_down').onclick=function(){
//        player.getVolume().then(volume => {
//            if(volume>0.1){
//                player.setVolume(volume-0.1).then(() => {
//                    console.log('Volume updated!');
//                  });   
//            }
//          });
//        
//           
//    };
//    document.getElementById('button_vol_up').onclick=function(){
//        player.getVolume().then(volume => {
//            if(volume<1){
//                player.setVolume(volume+0.1).then(() => {
//                    console.log('Volume updated!');
//                  });   
//            }
//          });
//          
//    };
//  
//}
//