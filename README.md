# Dragoon Log 🥷


## Tech Stack  🧨
**UI:** Razor, Dotnet, C# 

**Worker:** Mongodb, Dotnet, C#  


## Intro

Trata-se de um ultimate-log como principal funcionalidade , centralizar todos os logs de todos os serviços do ecossitema em um unico gerenciador de logs.
O Dragoon utiliza uma conexão via socket para receber os logs e realizar o save de uma banco NoSql como mongoDB.por padrão a porta configurada é a 15000, e no caso é só enviar o payload para o socker aberto.

O Dragoon por padrão pede um padrão de objeto para sua execução perfeita, o padrão é o seguinte abaixo:




```
 data = {
        "sender": self.service_name,
        "date": str(datetime.datetime.now()),
        "function": str(function),
        "user": user,
        "error": str(str.encode(str(error))),
        "payload": str(str.encode(str(payload))),
    }
```

trata-se de um exemplo em python, mas o payload é o mesmo.

1 . "sender" = "serviço que fez o envio do payload"

2 . "date": = "data e hora do envio"

3 . "function" = "nome do método ou funcionalidade que foi enviado"

4 . "user" = "usuário autenticado durante a operação"

5 . "error" = "erro ocorrido durante a operação, pode ser a exceção gerada"

6 . "payload" = "dados enviado na request"


 
