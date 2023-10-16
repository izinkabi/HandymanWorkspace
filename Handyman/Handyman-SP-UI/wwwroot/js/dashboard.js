//TODO - Animations
//TODO -    FORM VALIDATION
//TODO - Chart display

  //* Datetime for weather card 
  window.onload = function() {
    setInterval(function(){
        var date = new Date();
        var timeoptions = { hour: 'numeric', minute: 'numeric', hour12: true };
        var dateoptions = { weekday: 'long', month: 'long', day: 'numeric', year: 'numeric' };
        var displayDate = date.toLocaleDateString([],dateoptions);
        var displayTime = date.toLocaleTimeString([],timeoptions);

        
        document.getElementById('time').innerHTML = displayTime;
        document.getElementById('date').innerHTML = displayDate;
    }, 1000); // 1000 milliseconds = 1 second
}