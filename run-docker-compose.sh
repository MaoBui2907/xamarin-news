# Check if a file was existed
if [ -f ./api/VNlp/corpus/wiki.vi.vec ]; then
    echo "File vi.vec was existed"
else
    echo "File vi.vec was not existed"
    echo "Downloading vi.vec file"
    wget -P ./api/VNlp/corpus/ https://dl.fbaipublicfiles.com/fasttext/vectors-wiki/wiki.vi.vec
fi

if [ -f ./api/VNlp/model/wiki.vi.model ]; then
    echo "File vi.bin was existed"
else
    cd ./api/VNlp/
    if [ -f ./model ]; then
        echo "Folder model was existed"
    else
        echo "Folder model was not existed"
        mkdir model
    fi
    echo "File vi.bin was not existed"
    echo "Prepare vi.bin file"
    python VNlp.py
    cd ../../
fi

docker-compose -p news-summarizer-app -f ./docker-compose.dev.yml up -d