
// function startCountdown(targetDate, secondsSelector, minutesSelector, hoursSelector, daysSelector, countdownEndsSelector) {
//   const newDate = new Date(targetDate).getTime();
//   const countdown = setInterval(() => {
//       const date = new Date().getTime();
//       const diff = newDate - date;

//       const days = Math.floor(diff % (1000 * 60 * 60 * 24 * (365.25 / 12)) / (1000 * 60 * 60 * 24));
//       const hours = Math.floor(diff % (1000 * 60 * 60 * 24) / (1000 * 60 * 60));
//       const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
//       const seconds = Math.floor((diff % (1000 * 60)) / 1000);

//       document.querySelector(secondsSelector).innerHTML = seconds < 10 ? '0' + seconds : seconds;
//       document.querySelector(minutesSelector).innerHTML = minutes < 10 ? '0' + minutes : minutes;
//       document.querySelector(hoursSelector).innerHTML = hours < 10 ? '0' + hours : hours;
//       document.querySelector(daysSelector).innerHTML = days < 10 ? '0' + days : days;

//       if (diff <= 0) {
//           clearInterval(countdown);
//           document.querySelector(countdownEndsSelector).innerHTML = 'Countdown Ends';
//       }
//   }, 1000);
// }

// // Countdown configurations
// const countdownConfigurations = [
//   { targetDate: '2024-10-31 09:00:00', secondsSelector: '.seconds1', minutesSelector: '.minutes1', hoursSelector: '.hours1', daysSelector: '.days1', countdownEndsSelector: '.countdown1' },
//   { targetDate: '2024-11-30 12:00:00', secondsSelector: '.seconds2', minutesSelector: '.minutes2', hoursSelector: '.hours2', daysSelector: '.days2', countdownEndsSelector: '.countdown2' },
//   { targetDate: '2024-10-30 12:00:00', secondsSelector: '.seconds3', minutesSelector: '.minutes3', hoursSelector: '.hours3', daysSelector: '.days3', countdownEndsSelector: '.countdown3' },
//   { targetDate: '2024-09-30 12:00:00', secondsSelector: '.seconds4', minutesSelector: '.minutes4', hoursSelector: '.hours4', daysSelector: '.days4', countdownEndsSelector: '.countdown4' },
//   { targetDate: '2024-11-30 12:00:00', secondsSelector: '.seconds5', minutesSelector: '.minutes5', hoursSelector: '.hours5', daysSelector: '.days5', countdownEndsSelector: '.countdown5' }
// ];

// // Start countdown for each configuration
// countdownConfigurations.forEach(config => {
//   startCountdown(config.targetDate, config.secondsSelector, config.minutesSelector, config.hoursSelector, config.daysSelector, config.countdownEndsSelector);
// });

function startCountdown(targetClass, endDate) {
  var second = 1000,
      minute = second * 60,
      hour = minute * 60,
      day = hour * 24;

  var countDown = new Date(endDate).getTime(),
      x = setInterval(function () {
          var now = new Date().getTime(),
              distance = countDown - now;

          var daysElement = document.querySelector(targetClass + " .days"),
              hoursElement = document.querySelector(targetClass + " .hours"),
              minutesElement = document.querySelector(targetClass + " .minutes"),
              secondsElement = document.querySelector(targetClass + " .seconds");

          if (daysElement && hoursElement && minutesElement && secondsElement) {
              daysElement.innerText = Math.floor(distance / day);
              hoursElement.innerText = Math.floor((distance % day) / hour);
              minutesElement.innerText = Math.floor((distance % hour) / minute);
              secondsElement.innerText = Math.floor((distance % minute) / second);
          }
      }, second);
}

// Start the countdowns
startCountdown(".countdown:nth-child(1)", "5 09, 2025 00:00:00");
startCountdown(".countdown:nth-child(2)", "7 09, 2025 00:00:00");
startCountdown(".countdown:nth-child(3)", "2 09, 2025 00:00:00");
startCountdown(".countdown:nth-child(4)", "10 09, 2025 00:00:00");
startCountdown(".countdown:nth-child(5)", "2 09, 2025 00:00:00");
startCountdown(".countdown:nth-child(6)", "5 09, 2025 00:00:00");
startCountdown(".countdown:nth-child(7)", "7 09, 2025 00:00:00");
startCountdown(".countdown:nth-child(8)", "2 09, 2025 00:00:00");
startCountdown(".countdown:nth-child(9)", "10 09, 2025 00:00:00");
startCountdown(".countdown:nth-child(10)", "2 09, 2025 00:00:00");


// Start the countdowns
// var count = document.querySelectorAll('.count');
// if(count) {
//   startCountdown(".count:nth-child(1)", "5 09, 2024 00:00:00");
//   startCountdown(".count:nth-child(2)", "7 09, 2025 00:00:00");
//   startCountdown(".count:nth-child(3)", "2 09, 2025 00:00:00");
//   startCountdown(".count:nth-child(4)", "10 09, 2025 00:00:00");
//   startCountdown(".count:nth-child(5)", "2 09, 2025 00:00:00");
// }


