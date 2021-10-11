$("#btnListar").on("click", function () {
    debugger;
    var params = new Object();

    Get("UsuarioCursoOracle/ListarD", params).done(function (d) { 
        debugger; 
        let Data = d.data; 
        let Ordenes = d.data.lista;
 ;
        $("#tablausers").html("");
        for (i = 0; i < Ordenes.length; i++) {
            var $row = $("<tr></td>");
            $row.append("<td>" + Ordenes[i].OpridSemilla + "</td>");
            $row.append("<td>" + Ordenes[i].Unidneg + "</td>");
            $row.append("<td>" + Ordenes[i].ShortName + "</td>");
            $row.append("<td>" + Ordenes[i].UserName + "</td>");
            $row.append("<td>" + Ordenes[i].Emplid + "</td>");
            $row.append("<td>" + Ordenes[i].AcadCareer + "</td>");
            $row.append("<td>" + Ordenes[i].ClassSection + "</td>");
            $row.append("<td>" + Ordenes[i].Strm + "</td>");
            $row.append("<td>" + Ordenes[i].ClassNbr + "</td>");
            $row.append("<td>" + Ordenes[i].Roleid + "</td>");
            $row.append("<td>" + Ordenes[i].Suspend + "</td>");
            $row.append("<td>" + Ordenes[i].Sysdate + "</td>")
            $("#tablausers").append($row);


        }
    })


});

//public string OpridSemilla { get; set; }
//        public string Unidneg { get; set; }
//        public string ShortName { get; set; }
//        public string UserName { get; set; }
//        public int Emplid { get; set; }
//        public string AcadCareer { get; set; }
//        public string ClassSection { get; set; }
//        public string Strm { get; set; }
//        public int ClassNbr { get; set; }
//        public int Roleid { get; set; }
//        public int Suspend { get; set; }
//        public DateTime Sysdate { get; set; }