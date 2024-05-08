function shuffle(array) {
  var currentIndex = array.length,
    randomIndex;

  // While there remain elements to shuffle...
  while (0 !== currentIndex) {
    // Pick a remaining element...
    randomIndex = Math.floor(Math.random() * currentIndex);
    currentIndex--;
      
    // And swap it with the current element.
    [array[currentIndex], array[randomIndex]] = [
      array[randomIndex],
      array[currentIndex],
    ];
  }
    return array;
    console.log(array)
}

function spin() {
  // Play the sound
  wheel.play();
  // Inisialisasi variabel
  const box = document.getElementById("box");
  const element = document.getElementById("mainbox");
  let SelectedItem = "";

  // Shuffle 450 karena class box1 sudah ditambah 90 derajat diawal. minus 40 per item agar posisi panah pas ditengah.
  // Setiap item memiliki 12.5% kemenangan kecuali item sepeda yang hanya memiliki sekitar 4% peluang untuk menang.
  // Item berupa ipad dan samsung tab tidak akan pernah menang.
  // let Sepeda = shuffle([2210]); //Kemungkinan : 33% atau 1/3
  let zero = shuffle([1890, 2250, 2610]);
  let one = shuffle([1850, 2210, 2570]); //Kemungkinan : 100%
  let two = shuffle([1810, 2170, 2530]);
  let three = shuffle([1770, 2130, 2490]);
  let four = shuffle([1750, 2110, 2470]);
  let seven = shuffle([1630, 1990, 2350]);
   /* let six = shuffle([1570, 1930, 2290])*/
    let five = shuffle([ 4950])

  // Bentuk acak
  let Hasil = shuffle([
      zero[0],
      one[0],
    two[0],
    three[0],
    four[0],
    five[0],
      seven[0],
  ]);
   console.log(Hasil[0]);

  // Ambil value item yang terpilih
    if (zero.includes(Hasil[0])) SelectedItem = "0";
    if (one.includes(Hasil[0])) SelectedItem = "1";
  if (two.includes(Hasil[0])) SelectedItem = "2";
  if (three.includes(Hasil[0])) SelectedItem = "3";
  if (four.includes(Hasil[0])) SelectedItem = "4";
  if (five.includes(Hasil[0])) SelectedItem = "5";
    if (seven.includes(Hasil[0])) SelectedItem = "7";

  // Proses
    box.style.setProperty("transition", "all ease 5s");
    //alert(SelectedItem)
    //alert(Hasil[0])
    box.style.transform = "rotate(" + Hasil[0] + "deg)";
   /* box.style.transform = "rotate(" + "2210" + "deg)";*/
  element.classList.remove("animate");
  setTimeout(function () {
    element.classList.add("animate");
  }, 5000);

  // Munculkan Alert
  setTimeout(function () {
      applause.play();
      
    
          //swal(
          //    "Congratulations",
          //    "You Won The " + SelectedItem + "₹. \n Add on your Casino Wallet",
          //    "success"
          //);
      
          $.ajax({
              url: '../App/GetSpinValue',
              type: 'post',
              dataType: 'json',
              data: { spinAmt: SelectedItem},
             /* contentType: 'application/json; charset=utf-8',*/
              success: function (data) {

                 /* alert(data.Msg)*/
                  if (data.Msg == '1') {

                      if (SelectedItem != "0") {
                          swal(
                              "Congratulations",
                              "You Won The " + SelectedItem + " ₹. \n Add on your Casino Wallet",
                              "success"
                          );
                      }
                      else {
                          swal(
                              "OOPs",
                              "Please try on the next time",
                              "warning"
                          );

                      }
                    
                     
                  }
                  else {
                      swal(
                          "Failed",
                          "Please try on the next days ",
                          "warning"
                      );
                  }

              },
              error: function (XMLHttpRequest, textStatus, errorThrown) {

                  $("#showSpinner").hide();
              }
          });
     
  }, 5500);

 //  Delay and set to normal state
  setTimeout(function () {
    box.style.setProperty("transition", "initial");
    box.style.transform = "rotate(90deg)";
  }, 10000);

   
   /* return SelectedItem;*/


}
