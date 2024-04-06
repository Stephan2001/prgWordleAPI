Wordle api

available on github: https://github.com/Stephan2001/prgWordleAPI.git
available on Docker: st10219213/wordleapi:latest


This application has 2 methods, a get and post method.
The get method returns a random 5 letter word, the post method returns a array of true and false statements.
Depending on the words paramater recieved the method would return true and false, specifing if specific letters are contained and are in correct position of the randomly generated word.

if wordContains == true // the char is in the word
if wordContains == false // the char is not in the word

if charPosition == true // the char is in correct position
if charPosition == false // the char is not in correct position

get method -
URL: https://localhost:32778/api/wordle
paramaters: none
returns: string
E.g. (execute) -- returns: "trees"

post method-
URL: https://localhost:32778/api/wordle
paramaters: string (5 char lenght)
E.g.1 randonly generated word is "chain" 

curl -X 'POST' \
  'https://localhost:32778/api/wordle' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '"Woods"'
 returns:
[
  {
    "wordContains": false,
    "charPosition": false
  },
  {
    "wordContains": false,
    "charPosition": false
  },
  {
    "wordContains": false,
    "charPosition": false
  },
  {
    "wordContains": false,
    "charPosition": false
  },
  {
    "wordContains": false,
    "charPosition": false
  }
]

E.g.2
curl -X 'POST' \
  'https://localhost:32778/api/wordle' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '"chain"'
returns
[
  {
    "wordContains": true,
    "charPosition": true
  },
  {
    "wordContains": true,
    "charPosition": true
  },
  {
    "wordContains": true,
    "charPosition": true
  },
  {
    "wordContains": true,
    "charPosition": true
  },
  {
    "wordContains": true,
    "charPosition": true
  }
]