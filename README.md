# stock-quote-alert  

## Requisitos

O objetivo do sistema é avisar, via e-mail, caso a cotação de um ativo da B3 caia mais do que certo nível, ou suba acima de outro.

O programa deve ser uma aplicação de console (não há necessidade de interface gráfica).

Ele deve ser chamado via linha de comando com 3 parâmetros.

- O ativo a ser monitorado
- O preço de referência para venda
- O preço de referência para compra

Ex:

```bash
   stock-quote-alert.exe PETR4 22.67 22.59 
```

Ele deve ler de um arquivo de configuração com:  

- O e-mail de destino dos alertas
- As configurações de acesso ao servidor de SMTP que irá enviar o e-mail

O programa deve ficar continuamente monitorando a cotação do ativo enquanto estiver rodando.

## Arquivo de configurações

No repositório na pasta "Configs" há o template do arquivo de configurações denominado "emailSettings.json.template", basta tirar a extensão ".template" e modificar os campos de acordo com o email de destino dos alertas e com as configurações de acesso ao servidor de SMTP.  
O template em questão pode ser observado abaixo:  

```json
{
  "Username": "example@gmail.com",
  "Password": "password",
  "Host": "smtp.gmail.com",
  "Port": 587,
  "EnableSSL": true,
  "UseDefaultCredentials": false,
  "EmailTo": "example@gmail.com"
}
```
## Setup:
---------------------------  
1. Clone o repositório:

   ```bash
   $ git clone https://github.com/Matezcp/stock-quote-alert.git
   ```
   
   ou baixa como zip e o extraia.
2. Vá para a pasta "stock-quote-alert"
3. Arrume o arquivo de configurações conforme as instruções passadas na seção anterior "Arquivo de configurações"
4. Execute o .exe conforme escrito nos requistos, exemplo:  

   ```bash
   ./stock-quote-alert.exe PETR4 22.67 22.59
   ```
   
   Lembrando que os argumentos são: ticker do ativo, preço de venda e preço de compra, respectivamente.
