if [ "$#" -ne 3 ]; then
    echo "Uso: $0 <versao> <community> <enderecoIP>"
    exit 1
fi

VERSAO=$1
COMMUNITY=$2
ENDERECO_IP=$3

echo "Versão: $VERSAO"
echo "Community: $COMMUNITY"
echo "Endereço IP: $ENDERECO_IP"

echo "Executando o comando SNMP..."
snmpwalk -v "$VERSAO" -c "$COMMUNITY" "$ENDERECO_IP" > /tmp/saida.txt 2>&1

if [ $? -ne 0 ]; then
    echo "Erro ao executar o comando SNMP. Verifique /tmp/saida.txt para detalhes."
    exit 1
fi

echo "Comando SNMP executado com sucesso."
