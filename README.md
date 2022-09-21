# ASP.NET-Core-Practice
study ASP.NET 

- get
  - DI
    - Controller에서 private field (_post)를 사용하고 있었는데, 이는, controller에 hit 할때마다 새로 생성되는 것. 그러나 한번만 생성되면 되는 것이므로, 따로 service라는 폴더에 interface, class 만들고, singleton으로 생성하도록 연결