// Go to top

const toTop = document.querySelector(".to-top");

window.addEventListener("scroll", () => {
    if (window.pageYOffset > 100) {
        toTop.classList.add("active");
    }
    else {
        toTop.classList.remove("active");
    }
})

// Go to top - END


// Loading Design

const loading_two = document.getElementById("loader_two");
const loading = document.querySelector("#loader");
const body = document.querySelector("body");


const loadingFunc = () => {
    loading_two.style.display = "none";
}

const  animationLoadingFunc = () => {

    loadingFunc();

    let loadingDistance = -body.getBoundingClientRect().top;
    let loadingWidth = (loadingDistance / (body.getBoundingClientRect().height - document.documentElement.clientHeight)) * 100;
    let value = Math.floor(loadingWidth);

    loading.style.width = value + "%";

    if (value === 0) {
         loading.style.width = "100%";
    }



};


window.addEventListener("load",animationLoadingFunc);

const loadingNoneFunc = () => setTimeout(() =>{
    loading.style.display = "none";
},1200);

window.addEventListener("load", loadingNoneFunc);

// Loading Design - END


// Page Loader

var loader = document.getElementById("loadertop");


const  pageLoading = () => {


    let loadingDistance = -body.getBoundingClientRect().top;
    let loadingWidth = (loadingDistance / (body.getBoundingClientRect().height - document.documentElement.clientHeight)) * 100;
    let value = Math.floor(loadingWidth);

    loader.style.width = value + "%";

    if (value === 0) {
        loader.style.width = "100%";
    }



};


window.addEventListener("load",pageLoading);

const pageLoadingNone = () => setTimeout(() =>{
    loader.style.display = "none";
},1200);

window.addEventListener("load", pageLoadingNone);

// Page Loader - END



// Mouse Design

// var myMouse = document.getElementById("my-mouse");

// document.addEventListener('mousemove', function(e) {
    
//     myMouse.style.left = (e.pageX-25) + 'px';
//     myMouse.style.top = (e.pageY-15) + 'px';
// });

// Mouser Design - END

// Navbar position fix on scroll

$(document).ready(function() {
    $(window).scroll(function () {


        if ($(window).scrollTop() > 0) {
            $('#togNav').addClass('navbar-fixed-top');
        }

        if ($(window).scrollTop() < 1) {
            $('#togNav').removeClass('navbar-fixed-top');
        }
    });
});

// Navbar position fix on scroll - END


// Left Side Bar

function SidenavOpenClose() {
    var element = document.getElementById("mySidenav");
    element.classList.toggle("sidenavwidth");


    var icon = document.getElementById("icon");
    // icon.classList.toggle("bi-folder2-open");

    if (icon.classList.contains("bi-folder2")) {
        $("#icon").removeClass("bi-folder2").addClass("bi-folder2-open");
    }
    else{
        $("#icon").removeClass("bi-folder2-open").addClass("bi-folder2");
    }
}




var dropdown = document.getElementsByClassName("dropdown-btn");
var i;

for (i = 0; i < dropdown.length; i++) {
    dropdown[i].addEventListener("click", function() {
        this.classList.toggle("active");
        var dropdownContent = this.nextElementSibling;
        if (dropdownContent.style.display === "block") {
            dropdownContent.style.display = "none";
        } else {
            dropdownContent.style.display = "block";
        }
    });
}

// Left Side Bar - END





// Welcome Product Slider


$(document).ready(function (){
    $('.slider-product').owlCarousel({
        rtl:false,
        nav:false,
        dots:false,
        loop:true,
        margin:10,
        autoplay:true,
        autoplayTimeout:3500,
        autoplayHoverPause:true,
        responsiveClass:true,
        responsive:{
            0:{
                items:1,
                nav:true
            },
            600:{
                items:1,
                nav:false
            },
            1000:{
                items:1,
                nav:true,
                loop:true
            }
        }
    })
})

// Welcome Product Slider - END





/*  Welcome Timer  */

$(document).ready(function() {
    "use strict";
    $.fn.aksCountDown = function (options) {
        const aks = $(this);
        var settings = $.extend(
            {
                endTime: "",
                refresh: 1000,
                onEnd: function () {}
            },
            options
        );
        return this.each(function (i) {
            function endTimeCheck(d1, d2) {
                return (
                    d1.getFullYear() === d2.getFullYear() &&
                    d1.getMonth() === d2.getMonth() &&
                    d1.getDate() === d2.getDate()
                );
            }
            function countTimer() {
                var endTime = new Date(settings.endTime);
                endTime = Date.parse(endTime) / 1000;

                var now = new Date();
                now = Date.parse(now) / 1000;

                var timeLeft = endTime - now;

                var days = Math.floor(timeLeft / 86400);
                var hours = Math.floor((timeLeft - days * 86400) / 3600);
                var minutes = Math.floor((timeLeft - days * 86400 - hours * 3600) / 60);
                var seconds = Math.floor(
                    timeLeft - days * 86400 - hours * 3600 - minutes * 60
                );

                if (hours < "10") {
                    hours = "0" + hours;
                }
                if (minutes < "10") {
                    minutes = "0" + minutes;
                }
                if (seconds < "10") {
                    seconds = "0" + seconds;
                }

                $(aks).find("[data-days]").html(days);
                $(aks).find("[data-hours]").html(hours);
                $(aks).find("[data-minutes]").html(minutes);
                $(aks).find("[data-seconds]").html(seconds);
            }
            var now = new Date();
            var endTime = new Date(settings.endTime);

            if (endTimeCheck(now, endTime) === true) {
                settings.onEnd.call(aks);
            } else {
                setInterval(function () {
                    countTimer();
                }, settings.refresh);
            }
        });
    };

    $("#time").aksCountDown({
        //   endTime: "28 January 2022 10:00:00 GMT+01:00",
        //   endTime: document.querySelector(".endtime").innerText,
            endTime: document.querySelector("#endtime").value,
            onEnd: function () {
                $(this).html('<div class="timer-end">Finished Time</div>');
            }
        });

});



// Welcome Timer - END

// Brands Carousel

$(document).ready(function (){
    $('.brands-slider').owlCarousel({
        center:true,
        loop:true,
        nav:false,
        dots:false,
        margin:10,
        autoplay:true,
        autoplayTimeout:3500,
        autoplayHoverPause:true,
        responsiveClass:true,
        responsive:{
            0:{
                items:1,
                nav:true
            },
            400:{
                items:3,
                nav:true
            },
            600:{
                items:5,
                nav:false
            },
            1000:{
                items:7,
                nav:true,
                loop:true
            }
        }
    });
})

// Brands Carousel - END


// MY Product Carousel

$(document).ready(function (){
    $('.myproduct').owlCarousel({
        loop:true,
        nav:false,
        dots:false,
        margin:10,
        autoplay:true,
        autoplayTimeout:3500,
        autoplayHoverPause:true,
        responsiveClass:true,
        responsive:{
            0:{
                items:1,
                nav:true
            },
            400:{
                items:1,
                nav:true
            },
            770:{
                items:2,
                nav:true
            },
            1000:{
                items:3,
                nav:true,
                loop:true
            },
            1300:{
                items:4,
                nav:true,
                loop:true
            }
        }
    });
})

// MY Product Carousel - END

// Product Page Carousel

$(document).ready(function (){
    $('.product-page-carousel').owlCarousel({
        loop:true,
        nav:true,
        dots:true,
        margin:10,
        autoplay:true,
        autoplayTimeout:3500,
        autoplayHoverPause:true,
        responsiveClass:true,
        responsive:{
            0:{
                items:1,
                nav:true
            },
            400:{
                items:1,
                nav:true
            },
            770:{
                items:1,
                nav:true
            },
            1000:{
                items:1,
                nav:true,
                loop:true
            }
        }
    });
})

// Product Page Carousel - END

// Faceook Chat

var chatbox = document.getElementById('fb-customer-chat');
    chatbox.setAttribute("page_id", "104069092207921");
    chatbox.setAttribute("attribution", "biz_inbox");


window.fbAsyncInit = function() {
    FB.init({
        xfbml            : true,
        version          : 'v13.0'
    });
};

(function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = 'https://connect.facebook.net/en_US/sdk/xfbml.customerchat.js';
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));



// Facebook Chat - END



// Shopping Cart Js

/* Set values + misc */
var promoCode;
var promoPrice;
var fadeTime = 300;

/* Assign actions */
$('.quantity input').change(function() {
  updateQuantity(this);
});

$('.remove button').click(function() {
  removeItem(this);
});

$(document).ready(function() {
  updateSumItems();
});

$('.promo-code-cta').click(function() {

  promoCode = $('#promo-code').val();

  if (promoCode == '10off' || promoCode == '10OFF') {
    //If promoPrice has no value, set it as 10 for the 10OFF promocode
    if (!promoPrice) {
      promoPrice = 10;
    } else if (promoCode) {
      promoPrice = promoPrice * 1;
    }
  } else if (promoCode != '') {
    alert("Invalid Promo Code");
    promoPrice = 0;
  }
  //If there is a promoPrice that has been set (it means there is a valid promoCode input) show promo
  if (promoPrice) {
    $('.summary-promo').removeClass('hide');
    $('.promo-value').text(promoPrice.toFixed(2));
    recalculateCart(true);
  }
});

/* Recalculate cart */
function recalculateCart(onlyTotal) {
  var subtotal = 0;

  /* Sum up row totals */
  $('.basket-product').each(function() {
    subtotal += parseFloat($(this).children('.subtotal').text());
  });

  /* Calculate totals */
  var total = subtotal;

  //If there is a valid promoCode, and subtotal < 10 subtract from total
  var promoPrice = parseFloat($('.promo-value').text());
  if (promoPrice) {
    if (subtotal >= 10) {
      total -= promoPrice;
    } else {
      alert('Order must be more than £10 for Promo code to apply.');
      $('.summary-promo').addClass('hide');
    }
  }

  /*If switch for update only total, update only total display*/
  if (onlyTotal) {
    /* Update total display */
    $('.total-value').fadeOut(fadeTime, function() {
      $('#basket-total').html(total.toFixed(2));
      $('.total-value').fadeIn(fadeTime);
    });
  } else {
    /* Update summary display. */
    $('.final-value').fadeOut(fadeTime, function() {
      $('#basket-subtotal').html(subtotal.toFixed(2));
      $('#basket-total').html(total.toFixed(2));
      if (total == 0) {
        $('.checkout-cta').fadeOut(fadeTime);
      } else {
        $('.checkout-cta').fadeIn(fadeTime);
      }
      $('.final-value').fadeIn(fadeTime);
    });
  }
}

/* Update quantity */
function updateQuantity(quantityInput) {
  /* Calculate line price */
  var productRow = $(quantityInput).parent().parent();
  var price = parseFloat(productRow.children('.price').text());
  var quantity = $(quantityInput).val();
  var linePrice = price * quantity;

  /* Update line price display and recalc cart totals */
  productRow.children('.subtotal').each(function() {
    $(this).fadeOut(fadeTime, function() {
      $(this).text(linePrice.toFixed(2));
      recalculateCart();
      $(this).fadeIn(fadeTime);
    });
  });

  productRow.find('.item-quantity').text(quantity);
  updateSumItems();
}

function updateSumItems() {
  var sumItems = 0;
  $('.quantity input').each(function() {
    sumItems += parseInt($(this).val());
  });
  $('.total-items').text(sumItems);
}

/* Remove item from cart */
function removeItem(removeButton) {
  /* Remove row from DOM and recalc cart total */
  var productRow = $(removeButton).parent().parent();
  productRow.slideUp(fadeTime, function() {
    productRow.remove();
    recalculateCart();
    updateSumItems();
  });
}

// Shopping Cart Js - END


// Login Register JS

const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".rlcontainer");

sign_up_btn.addEventListener("click", () => {
  container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener("click", () => {
  container.classList.remove("sign-up-mode");
});



// Login Register JS END